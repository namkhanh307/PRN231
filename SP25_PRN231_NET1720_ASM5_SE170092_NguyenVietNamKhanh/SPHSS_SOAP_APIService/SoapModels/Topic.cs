﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SPHSS_SOAP_APIService.SoapModels;

[DataContract]
public partial class Topic
{
    [Key]
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string Description { get; set; }
    [DataMember]
    public DateTime? CreateAt { get; set; }
    [DataMember]
    public DateTime? UpdateAt { get; set; }

}