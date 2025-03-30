    [HttpGet("{id}")] // GET api/users/5

//BE
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.1" />
    <PackageReference Include="Microsoft.AspNetCore.OData" Version="8.2.5" />
//Repo
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.5" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.5" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.5">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0" />
//appsettings

  "Jwt": {
    "Key": "0ccfeb299b126a479a64630e2d34e9e91e5fcbcaea8ac9e3347e224b0557a53e",
    "Issuer": "https://localhost:7097",
    "Audience": "https://localhost:7097"
  }
//Program.cs
builder.Services.AddScoped(typeof(IBaseDAO<>), typeof(BaseDAO<>));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
});

////JWT Config
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    ////JWT Config
    option.DescribeAllParametersInCamelCase();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<CosmeticInformation>("CosmeticInformation");
    return odataBuilder.GetEdmModel();
}
builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy().Expand().SetMaxTop(null).Count();
    options.AddRouteComponents("odata", GetEdmModel());
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//DBContext

public static string? GetConnectionString(string connectionStringName)
{
    var config = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

    string? connectionString = config.GetConnectionString(connectionStringName);
    return connectionString;
}

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

//IBaseRepo
public interface IBaseRepo<T> where T : class
{
    IQueryable<T> Entities { get; }
    Task<T?> GetById(string id, string pk, Func<IQueryable<T>, IQueryable<T>>? include = null);
    Task<IList<T>> Get(Func<IQueryable<T>, IQueryable<T>>? include = null);
    Task<bool> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
    Task<PremierLeagueAccount?> Login(string email, string password);
}

//BaseRepo
public class BaseRepo<T> : IBaseRepo<T> where T : class
{
    protected readonly EnglishPremierLeague2024DBContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepo()
    {
        _context = new EnglishPremierLeague2024DBContext();
        _dbSet = _context.Set<T>();
    }
    public IQueryable<T> Entities => _context.Set<T>();

    public async Task<IList<T>> Get(Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        IQueryable<T> query = _dbSet;
        if (include != null)
        {
            query = include(query);
        }
        return await query.ToListAsync();
    }

    public async Task<T?> GetById(string id, string pk, Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        IQueryable<T> query = _dbSet;
        if (include != null)
        {
            query = include(query);
        }
        return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, pk) == id);
    }

    public async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<PremierLeagueAccount?> Login(string email, string password)
    {
        return await _context.PremierLeagueAccounts.SingleOrDefaultAsync(s => s.EmailAddress == email && s.Password == password);
    }
}


//LoginService
public class PremierLeagueAccountService
{
    private readonly IBaseRepo<PremierLeagueAccount> _repo;
    private readonly IConfiguration _configuration;
    public PremierLeagueAccountService(IBaseRepo<PremierLeagueAccount> repo, IConfiguration configuration)
    {
        _repo = repo;
        _configuration = configuration;
    }
    public async Task<string> Login(string email, string password)
    {
        PremierLeagueAccount? account = await _repo.Login(email, password);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"]
                , _configuration["Jwt:Audience"]
                , new Claim[]
                {
                        new(ClaimTypes.Email, account.EmailAddress),
                        new(ClaimTypes.Role, account.Role.ToString()),
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }

}
//MainEntityService
public class FootballPlayerService
{
    private readonly IBaseRepo<FootballPlayer> _repo;
private readonly IBaseRepo<FootballClub> _subRepo;
private readonly string MainEntityId = "FootballPlayerId";
private readonly string SubEntityId = "FootballClubId";

public FootballPlayerService(IBaseRepo<FootballPlayer> repo, IBaseRepo<FootballClub> subRepo)
{
    _repo = repo;
    _subRepo = subRepo;
}

public async Task<string> Add(FootballPlayer request)
{
    var existed = await _repo.GetById(request.FootballPlayerId, MainEntityId);
    if (existed != null)
    {
        return "Already exist ...!";
    }
    if (string.IsNullOrWhiteSpace(request.FullName) ||
        string.IsNullOrWhiteSpace(request.Nomination) ||
        string.IsNullOrWhiteSpace(request.Achievements) || string.IsNullOrWhiteSpace(request.PlayerExperiences) || request.Birthday == null)
    {
        return "All fields are required";
    }

    if (!Regex.IsMatch(request.FullName, @"^([A-Z][a-zA-Z0-9@#]*\s)*[A-Z][a-zA-Z0-9@#]*$"))
    {
        return "Fullname must start with a capital letter in each word and only include letters, numbers, space, @, and #.";
    }
    if (request.Birthday >= new DateTime(2007, 1, 1))
    {
        return "Birthday must < 01-01-2007";
    }

    // Validate length
    if (request.Nomination.Length < 9 || request.Achievements.Length > 100 ||
        request.Nomination.Length < 9 || request.Achievements.Length > 100)
    {
        return "Nomination and Achievements must be between 9 and 100 characters.";
    }

    if (await _subRepo.GetById(request.FootballClubId, SubEntityId) == null)
    {
        return "Not exist ...!";

    }

    FootballPlayer data = new FootballPlayer()
    {
        FootballPlayerId = request.FootballPlayerId,
        FullName = request.FullName,
        Nomination = request.Nomination,
        Achievements = request.Achievements,
        FootballClubId = request.FootballClubId,
        Birthday = request.Birthday,
        PlayerExperiences = request.PlayerExperiences,
    };

    await _repo.Add(data);
    return "Add success!";
}

public async Task<IEnumerable<MainEntityVM>> Get()
{
    var listData = await _repo.Get(a => a.Include(b => b.FootballClub));
    var result = listData.ToList().Select(a => new MainEntityVM()
    {
        PlayerExperiences = a.PlayerExperiences,
        Birthday = a.Birthday,
        FootballClubId = a.FootballClubId,
        Achievements = a.Achievements,
        Nomination = a.Nomination,
        FullName = a.FullName,
        FootballPlayerId = a.FootballPlayerId,
        FootballClubName = a.FootballClub.ClubName,
    }).OrderByDescending(b => b.FootballPlayerId);
    return result;
}

public async Task<string> Update(FootballPlayer request)
{
    var existed = await _repo.GetById(request.FootballPlayerId, MainEntityId);
    if (existed == null)
    {
        return "Not exist ...!";
    }
    if (string.IsNullOrWhiteSpace(request.FullName) ||
         string.IsNullOrWhiteSpace(request.Nomination) ||
         string.IsNullOrWhiteSpace(request.Achievements) || string.IsNullOrWhiteSpace(request.PlayerExperiences) || request.Birthday == null)
    {
        return "All fields are required";
    }

    if (!Regex.IsMatch(request.FullName, @"^([A-Z][a-zA-Z0-9@#]*\s)*[A-Z][a-zA-Z0-9@#]*$"))
    {
        return "Fullname must start with a capital letter in each word and only include letters, numbers, space, @, and #.";
    }
    if (request.Birthday >= new DateTime(2007, 1, 1))
    {
        return "Birthday must < 01-01-2007";
    }

    if (request.Nomination.Length < 9 || request.Achievements.Length > 100 ||
        request.Nomination.Length < 9 || request.Achievements.Length > 100)
    {
        return "Nomination and Achievements must be between 9 and 100 characters.";
    }

    if (await _subRepo.GetById(request.FootballClubId, SubEntityId) == null)
    {
        return "Not exist ...!";

    }

    existed.Nomination = request.Nomination;
    existed.Achievements = request.Achievements;
    existed.FullName = request.FullName;
    existed.Birthday = request.Birthday;
    existed.PlayerExperiences = request.PlayerExperiences;
    existed.FootballClubId = request.FootballClubId;
    await _repo.Update(existed);
    return "Update success!";
}

public async Task<string> Delete(string id)
{
    var existed = await _repo.GetById(id, MainEntityId);
    if (existed == null)
    {
        return "Not exist";
    }
    await _repo.Delete(existed);
    return "Delete success!";
}

public async Task<IEnumerable<MainEntityVM>> Search(string? nomination, string? achievement)
{
    return await _repo.Entities.Include(u => u.FootballClub).Where(u => (string.IsNullOrWhiteSpace(achievement) || u.Achievements.Contains(achievement)) &&
                        (string.IsNullOrWhiteSpace(nomination) || u.Nomination.Contains(nomination))).Select(g => new MainEntityVM
                        {
                            Achievements = g.Achievements,
                            FullName = g.FullName,
                            Birthday = g.Birthday,
                            FootballClubId = g.FootballClubId,
                            FootballClubName = g.FootballClub.ClubName,
                            FootballPlayerId = g.FootballPlayerId,
                            Nomination = g.Nomination,
                            PlayerExperiences = g.PlayerExperiences,
                        }).ToListAsync();
}


public async Task<IEnumerable<FootballClub>> GetSubEntity()
{
    return await _subRepo.Get();
}

public async Task<MainEntityVM> GetById(string id)
{
    var exist = await _repo.GetById(id, MainEntityId, a => a.Include(b => b.FootballClub));
    if (exist == null)
    {
        return new MainEntityVM();
    }
    return new MainEntityVM
    {
        Nomination = exist.Nomination,
        Achievements = exist.Achievements,
        FullName = exist.FullName,
        Birthday = exist.Birthday,
        PlayerExperiences = exist.PlayerExperiences,
        FootballClubId = exist.FootballClubId,
        FootballClubName = exist.FootballClub.ClubName,
        FootballPlayerId = exist.FootballPlayerId
    };
}
    }

//MainEntityController
public class FootballPlayersController : ControllerBase
{
    private readonly FootballPlayerService _service;

    public FootballPlayersController(FootballPlayerService service)
    {
        _service = service;
    }
    [Authorize(Roles = "1,2")]
    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        var result = await _service.Get();
        return Ok(result);
    }
    [Authorize(Roles = "1")]
    [HttpGet("getById")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _service.GetById(id);
        return Ok(result);
    }
    [Authorize(Roles = "1,2")]
    [HttpGet("search")]
    [EnableQuery]
    public async Task<IActionResult> Search(string? nomination, string? achievement)
    {
        var result = await _service.Search(nomination, achievement);
        return Ok(result);
    }
    [Authorize(Roles = "1")]
    [HttpPost("add")]
    public async Task<IActionResult> Add(FootballPlayer request)
    {
        var result = await _service.Add(request);
        return Ok(result);
    }
    [Authorize(Roles = "1")]
    [HttpPut("update")]
    public async Task<IActionResult> Update(FootballPlayer request)
    {
        var result = await _service.Update(request);
        return Ok(result);
    }
    [Authorize(Roles = "1")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}

//LoginController
public class PremierLeagueAccountsController : ControllerBase
{
    private readonly PremierLeagueAccountService _service;

    public PremierLeagueAccountsController(PremierLeagueAccountService service)
    {
        _service = service;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _service.Login(request.email, request.password);
        if (user == null || user.Result == null)
            return Unauthorized();
        return Ok(user.Result);
    }
}
public sealed record LoginRequest(string email, string password);

