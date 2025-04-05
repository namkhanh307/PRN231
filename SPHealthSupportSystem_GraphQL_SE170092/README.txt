
query {
  psychologyTheories {
    id
    name
    description
  }
}
query {
  topics {
    id
    name
  }
}
query {
  psychologyTheory(id: 1) {
    id
    name
    description
    author
    yearPublished
    theoryType
    principle
    application
    relatedTheory
    criticism
  }
}

query {
  topic(id: 1) {
    id
    name
    description
  }
}

query {
  searchPsychologyTheories(name: "thuyết", topic: "", author: "jo") {
    id
    name
    description
    topicId
    author
    yearPublished
    createAt
    updateAt
  }
}

mutation {
  psychologyTheory{
     createPsychologyTheory(psychologyTheoryInput: {
    id: 2,
    name: "Cognitive Dissonance",
    description: "Theory by Leon Festinger",
    topicId: 1,
    author: "Leon Festinger",
    yearPublished: 1957,
    theoryType: "Cognitive",
    principle: "People experience discomfort.",
    application: "Marketing, decision-making, social psychology",
    relatedTheory: "Self-perception theory",
    criticism: "Limited empirical evidence"
  })
  }
}
mutation {
  psychologyTheory{
     updatePsychologyTheory(psychologyTheoryInput: {
    id: 2,
    name: "hahahaha",
    description: "Theory by Leon Festinger",
    topicId: 1,
    author: "Leon Festinger",
    yearPublished: 1957,
    theoryType: "Cognitive",
    principle: "People experience discomfort.",
    application: "Marketing, decision-making, social psychology",
    relatedTheory: "Self-perception theory",
    criticism: "Limited empirical evidence"
  })
  }
}
mutation {
  psychologyTheory{
     deletePsychologyTheory(
    id: 2,
  )
  }
}