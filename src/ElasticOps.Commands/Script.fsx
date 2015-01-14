#I @"E:\SourceTree\elasticops\src\ElasticOps.Com\bin\Debug"

open System.Reflection
open System
open System.Linq
//open ElasticOps.Com

let types = Assembly.Load("ElasticOps.Com").GetTypes();

let res1 ="";


let res = """
{
   "_scroll_id":"c2Nhbjs1OzEwMTp3azNLOVVNVlJ1U3Q5c0dUaXJYYzB3OzEwMzp3azNLOVVNVlJ1U3Q5c0dUaXJYYzB3OzEwMjp3azNLOVVNVlJ1U3Q5c0dUaXJYYzB3Ozk5OndrM0s5VU1WUnVTdDlzR1RpclhjMHc7MTAwOndrM0s5VU1WUnVTdDlzR1RpclhjMHc7MTt0b3RhbF9oaXRzOjg2MDg7",
   "took":1,
   "timed_out":false,
   "_shards":{
      "total":5,
      "successful":5,
      "failed":0
   },
   "hits":{
      "total":8608,
      "max_score":0.0,
      "hits":[
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Johnny Spillane.2010",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Nordic Combined"
                     ],
                     "code":[
                        "nordic combined"
                     ],
                     "props":{

                     },
                     "key":[
                        "nordiccombined"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "29"
                     ]
                  },
                  "won":{
                     "name":[
                        "Johnny Spillane"
                     ],
                     "code":[
                        "johnny spillane"
                     ],
                     "props":{

                     },
                     "key":[
                        "johnnyspillane"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Johnny Spillane.2010",
               "@timestamp":1406852406464,
               "@when":1406852280914,
               "@metaKey":"Otr4zDzYSt67coM1o2rCQg",
               "@what":{
                  "Year":"2010",
                  "Closing Ceremony Date":"2/28/10",
                  "Age":"29",
                  "Athlete":"Johnny Spillane",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"0",
                  "Sport":"Nordic Combined",
                  "Country":"United States",
                  "Silver Medals":"3"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Johnny Spillane.2010",
               "@whenCreated":1406852403232,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Felix Gottwald.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2006":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Nordic Combined"
                     ],
                     "code":[
                        "nordic combined"
                     ],
                     "props":{

                     },
                     "key":[
                        "nordiccombined"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "30"
                     ]
                  },
                  "won":{
                     "name":[
                        "Felix Gottwald"
                     ],
                     "code":[
                        "felix gottwald"
                     ],
                     "props":{

                     },
                     "key":[
                        "felixgottwald"
                     ]
                  },
                  "country":{
                     "name":[
                        "Austria"
                     ],
                     "code":[
                        "at"
                     ],
                     "props":{

                     },
                     "key":[
                        "at"
                     ]
                  }
               },
               "@code":"Felix Gottwald.2006",
               "@timestamp":1406852406599,
               "@when":1406852281661,
               "@metaKey":"5_AzbB0cTCiu1n3pmsE-Aw",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"30",
                  "Athlete":"Felix Gottwald",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Nordic Combined",
                  "Country":"Austria",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Felix Gottwald.2006",
               "@whenCreated":1406852403264,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Samppa Lajunen.2002",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Nordic Combined"
                     ],
                     "code":[
                        "nordic combined"
                     ],
                     "props":{

                     },
                     "key":[
                        "nordiccombined"
                     ]
                  },
                  "2002":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "22"
                     ]
                  },
                  "won":{
                     "name":[
                        "Samppa Lajunen"
                     ],
                     "code":[
                        "samppa lajunen"
                     ],
                     "props":{

                     },
                     "key":[
                        "samppalajunen"
                     ]
                  },
                  "country":{
                     "name":[
                        "Finland"
                     ],
                     "code":[
                        "fi"
                     ],
                     "props":{

                     },
                     "key":[
                        "fi"
                     ]
                  }
               },
               "@code":"Samppa Lajunen.2002",
               "@timestamp":1406852406888,
               "@when":1406852284113,
               "@metaKey":"FV4jma8HSWOhGPQ3pzgDqA",
               "@what":{
                  "Year":"2002",
                  "Closing Ceremony Date":"2/24/02",
                  "Age":"22",
                  "Athlete":"Samppa Lajunen",
                  "Total Medals":"3",
                  "Gold Medals":"3",
                  "Bronze Medals":"0",
                  "Sport":"Nordic Combined",
                  "Country":"Finland",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Samppa Lajunen.2002",
               "@whenCreated":1406852403324,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Aly Raisman.2012",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Gymnastics"
                     ],
                     "code":[
                        "gymnastics"
                     ],
                     "props":{

                     },
                     "key":[
                        "gymnastics"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "18"
                     ]
                  },
                  "2012":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "won":{
                     "name":[
                        "Aly Raisman"
                     ],
                     "code":[
                        "aly raisman"
                     ],
                     "props":{

                     },
                     "key":[
                        "alyraisman"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Aly Raisman.2012",
               "@timestamp":1406852406982,
               "@when":1406852284863,
               "@metaKey":"_wdF1ZbEQ5e4JRfmEBRHcA",
               "@what":{
                  "Year":"2012",
                  "Closing Ceremony Date":"8/12/12",
                  "Age":"18",
                  "Athlete":"Aly Raisman",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"1",
                  "Sport":"Gymnastics",
                  "Country":"United States",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Aly Raisman.2012",
               "@whenCreated":1406852403375,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Cheng Fei.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2008":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Gymnastics"
                     ],
                     "code":[
                        "gymnastics"
                     ],
                     "props":{

                     },
                     "key":[
                        "gymnastics"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "20"
                     ]
                  },
                  "won":{
                     "name":[
                        "Cheng Fei"
                     ],
                     "code":[
                        "cheng fei"
                     ],
                     "props":{

                     },
                     "key":[
                        "chengfei"
                     ]
                  },
                  "country":{
                     "name":[
                        "China"
                     ],
                     "code":[
                        "cn"
                     ],
                     "props":{

                     },
                     "key":[
                        "cn"
                     ]
                  }
               },
               "@code":"Cheng Fei.2008",
               "@timestamp":1406852407250,
               "@when":1406852287105,
               "@metaKey":"iBiAvnGRRNau00jX5_3baA",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"20",
                  "Athlete":"Cheng Fei",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"2",
                  "Sport":"Gymnastics",
                  "Country":"China",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Cheng Fei.2008",
               "@whenCreated":1406852403440,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"An Hyeon-Su.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2006":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Short-Track Speed Skating"
                     ],
                     "code":[
                        "short-track speed skating"
                     ],
                     "props":{

                     },
                     "key":[
                        "short-trackspeedskating"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "20"
                     ]
                  },
                  "won":{
                     "name":[
                        "An Hyeon-Su"
                     ],
                     "code":[
                        "an hyeon-su"
                     ],
                     "props":{

                     },
                     "key":[
                        "anhyeon-su"
                     ]
                  },
                  "country":{
                     "name":[
                        "South Korea"
                     ],
                     "code":[
                        "kr"
                     ],
                     "props":{

                     },
                     "key":[
                        "kr"
                     ]
                  }
               },
               "@code":"An Hyeon-Su.2006",
               "@timestamp":1406852331425,
               "@when":1406852205735,
               "@metaKey":"QfAE76FQQY-Z1S5LTaXUug",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"20",
                  "Athlete":"An Hyeon-Su",
                  "Total Medals":"4",
                  "Gold Medals":"3",
                  "Bronze Medals":"1",
                  "Sport":"Short-Track Speed Skating",
                  "Country":"South Korea",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.An Hyeon-Su.2006",
               "@whenCreated":1406852319412,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Dmitry Sautin.2000",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Diving"
                     ],
                     "code":[
                        "diving"
                     ],
                     "props":{

                     },
                     "key":[
                        "diving"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "26"
                     ]
                  },
                  "won":{
                     "name":[
                        "Dmitry Sautin"
                     ],
                     "code":[
                        "dmitry sautin"
                     ],
                     "props":{

                     },
                     "key":[
                        "dmitrysautin"
                     ]
                  },
                  "country":{
                     "name":[
                        "Russia"
                     ],
                     "code":[
                        "ru"
                     ],
                     "props":{

                     },
                     "key":[
                        "ru"
                     ]
                  },
                  "2000":{
                     "name":[
                        "Gold Medals",
                        "Bronze Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "bronze medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "bronzemedals",
                        "silvermedals"
                     ]
                  }
               },
               "@code":"Dmitry Sautin.2000",
               "@timestamp":1406852331653,
               "@when":1406852207953,
               "@metaKey":"uVQFGqZpQ1S_DU9CRZZhgQ",
               "@what":{
                  "Year":"2000",
                  "Closing Ceremony Date":"10/1/00",
                  "Age":"26",
                  "Athlete":"Dmitry Sautin",
                  "Total Medals":"4",
                  "Gold Medals":"1",
                  "Bronze Medals":"2",
                  "Sport":"Diving",
                  "Country":"Russia",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Dmitry Sautin.2000",
               "@whenCreated":1406852319538,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Janica Kostelic.2002",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Alpine Skiing"
                     ],
                     "code":[
                        "alpine skiing"
                     ],
                     "props":{

                     },
                     "key":[
                        "alpineskiing"
                     ]
                  },
                  "2002":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "20"
                     ]
                  },
                  "won":{
                     "name":[
                        "Janica Kostelic"
                     ],
                     "code":[
                        "janica kostelic"
                     ],
                     "props":{

                     },
                     "key":[
                        "janicakostelic"
                     ]
                  },
                  "country":{
                     "name":[
                        "Croatia"
                     ],
                     "code":[
                        "hr"
                     ],
                     "props":{

                     },
                     "key":[
                        "hr"
                     ]
                  }
               },
               "@code":"Janica Kostelic.2002",
               "@timestamp":1406852332342,
               "@when":1406852210973,
               "@metaKey":"_7D2lhjMQj6tXBnp6vndtw",
               "@what":{
                  "Year":"2002",
                  "Closing Ceremony Date":"2/24/02",
                  "Age":"20",
                  "Athlete":"Janica Kostelic",
                  "Total Medals":"4",
                  "Gold Medals":"3",
                  "Bronze Medals":"0",
                  "Sport":"Alpine Skiing",
                  "Country":"Croatia",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Janica Kostelic.2002",
               "@whenCreated":1406852319752,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Matt Grevers.2012",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "27"
                     ]
                  },
                  "2012":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "won":{
                     "name":[
                        "Matt Grevers"
                     ],
                     "code":[
                        "matt grevers"
                     ],
                     "props":{

                     },
                     "key":[
                        "mattgrevers"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Matt Grevers.2012",
               "@timestamp":1406852332748,
               "@when":1406852214057,
               "@metaKey":"RA4PWo4ATyaUlJF-Pt29yg",
               "@what":{
                  "Year":"2012",
                  "Closing Ceremony Date":"8/12/12",
                  "Age":"27",
                  "Athlete":"Matt Grevers",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Swimming",
                  "Country":"United States",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Matt Grevers.2012",
               "@whenCreated":1406852319908,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Therese Alshammar.2000",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "23"
                     ]
                  },
                  "won":{
                     "name":[
                        "Therese Alshammar"
                     ],
                     "code":[
                        "therese alshammar"
                     ],
                     "props":{

                     },
                     "key":[
                        "theresealshammar"
                     ]
                  },
                  "country":{
                     "name":[
                        "Sweden"
                     ],
                     "code":[
                        "se"
                     ],
                     "props":{

                     },
                     "key":[
                        "se"
                     ]
                  }
               },
               "@code":"Therese Alshammar.2000",
               "@timestamp":1406852338240,
               "@when":1406852244342,
               "@metaKey":"WA08BDyjSAacCy8Em73WKA",
               "@what":{
                  "Year":"2000",
                  "Closing Ceremony Date":"10/1/00",
                  "Age":"23",
                  "Athlete":"Therese Alshammar",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"1",
                  "Sport":"Swimming",
                  "Country":"Sweden",
                  "Silver Medals":"2"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Therese Alshammar.2000",
               "@whenCreated":1406852321429,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Alain Bernard.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2008":{
                     "name":[
                        "Silver Medals",
                        "Bronze Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "silver medals",
                        "bronze medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "silvermedals",
                        "bronzemedals",
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "25"
                     ]
                  },
                  "won":{
                     "name":[
                        "Alain Bernard"
                     ],
                     "code":[
                        "alain bernard"
                     ],
                     "props":{

                     },
                     "key":[
                        "alainbernard"
                     ]
                  },
                  "country":{
                     "name":[
                        "France"
                     ],
                     "code":[
                        "fr"
                     ],
                     "props":{

                     },
                     "key":[
                        "fr"
                     ]
                  }
               },
               "@code":"Alain Bernard.2008",
               "@timestamp":1406852333643,
               "@when":1406852221261,
               "@metaKey":"HvsoQxbmS1itWZxrPI7inQ",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"25",
                  "Athlete":"Alain Bernard",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"1",
                  "Sport":"Swimming",
                  "Country":"France",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Alain Bernard.2008",
               "@whenCreated":1406852320210,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"László Cseh Jr..2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "22"
                     ]
                  },
                  "won":{
                     "name":[
                        "László Cseh Jr."
                     ],
                     "code":[
                        "lászló cseh jr."
                     ],
                     "props":{

                     },
                     "key":[
                        "lászlócsehjr."
                     ]
                  },
                  "country":{
                     "name":[
                        "Hungary"
                     ],
                     "code":[
                        "hu"
                     ],
                     "props":{

                     },
                     "key":[
                        "hu"
                     ]
                  }
               },
               "@code":"László Cseh Jr..2008",
               "@timestamp":1406852333724,
               "@when":1406852221983,
               "@metaKey":"_lHRKIaQTxKqGpY633PFhw",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"22",
                  "Athlete":"László Cseh Jr.",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"0",
                  "Sport":"Swimming",
                  "Country":"Hungary",
                  "Silver Medals":"3"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.László Cseh Jr..2008",
               "@whenCreated":1406852320231,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Leisel Jones.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2008":{
                     "name":[
                        "Silver Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "silver medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "silvermedals",
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "22"
                     ]
                  },
                  "won":{
                     "name":[
                        "Leisel Jones"
                     ],
                     "code":[
                        "leisel jones"
                     ],
                     "props":{

                     },
                     "key":[
                        "leiseljones"
                     ]
                  },
                  "country":{
                     "name":[
                        "Australia"
                     ],
                     "code":[
                        "au"
                     ],
                     "props":{

                     },
                     "key":[
                        "au"
                     ]
                  }
               },
               "@code":"Leisel Jones.2008",
               "@timestamp":1406852334274,
               "@when":1406852224909,
               "@metaKey":"_QtGQK2iQ_q7Fz4eXVlQRA",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"22",
                  "Athlete":"Leisel Jones",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Swimming",
                  "Country":"Australia",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Leisel Jones.2008",
               "@whenCreated":1406852320348,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Andrew Lauterstein.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "21"
                     ]
                  },
                  "won":{
                     "name":[
                        "Andrew Lauterstein"
                     ],
                     "code":[
                        "andrew lauterstein"
                     ],
                     "props":{

                     },
                     "key":[
                        "andrewlauterstein"
                     ]
                  },
                  "country":{
                     "name":[
                        "Australia"
                     ],
                     "code":[
                        "au"
                     ],
                     "props":{

                     },
                     "key":[
                        "au"
                     ]
                  }
               },
               "@code":"Andrew Lauterstein.2008",
               "@timestamp":1406852334416,
               "@when":1406852226352,
               "@metaKey":"yrdx9o2lS0CxqkT4icfAQQ",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"21",
                  "Athlete":"Andrew Lauterstein",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"2",
                  "Sport":"Swimming",
                  "Country":"Australia",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Andrew Lauterstein.2008",
               "@whenCreated":1406852320423,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Aaron Peirsol.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2008":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "25"
                     ]
                  },
                  "won":{
                     "name":[
                        "Aaron Peirsol"
                     ],
                     "code":[
                        "aaron peirsol"
                     ],
                     "props":{

                     },
                     "key":[
                        "aaronpeirsol"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Aaron Peirsol.2008",
               "@timestamp":1406852334684,
               "@when":1406852228513,
               "@metaKey":"Zw2ons_eRPac1d-t9-PIsQ",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"25",
                  "Athlete":"Aaron Peirsol",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Swimming",
                  "Country":"United States",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Aaron Peirsol.2008",
               "@whenCreated":1406852320537,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Yang Yilin.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2008":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Gymnastics"
                     ],
                     "code":[
                        "gymnastics"
                     ],
                     "props":{

                     },
                     "key":[
                        "gymnastics"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "15"
                     ]
                  },
                  "won":{
                     "name":[
                        "Yang Yilin"
                     ],
                     "code":[
                        "yang yilin"
                     ],
                     "props":{

                     },
                     "key":[
                        "yangyilin"
                     ]
                  },
                  "country":{
                     "name":[
                        "China"
                     ],
                     "code":[
                        "cn"
                     ],
                     "props":{

                     },
                     "key":[
                        "cn"
                     ]
                  }
               },
               "@code":"Yang Yilin.2008",
               "@timestamp":1406852407349,
               "@when":1406852288576,
               "@metaKey":"olNpjFVuTa2BUFRhAblVww",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"15",
                  "Athlete":"Yang Yilin",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"2",
                  "Sport":"Gymnastics",
                  "Country":"China",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Yang Yilin.2008",
               "@whenCreated":1406852403489,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Yekaterina Lobaznyuk.2000",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Gymnastics"
                     ],
                     "code":[
                        "gymnastics"
                     ],
                     "props":{

                     },
                     "key":[
                        "gymnastics"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "17"
                     ]
                  },
                  "won":{
                     "name":[
                        "Yekaterina Lobaznyuk"
                     ],
                     "code":[
                        "yekaterina lobaznyuk"
                     ],
                     "props":{

                     },
                     "key":[
                        "yekaterinalobaznyuk"
                     ]
                  },
                  "country":{
                     "name":[
                        "Russia"
                     ],
                     "code":[
                        "ru"
                     ],
                     "props":{

                     },
                     "key":[
                        "ru"
                     ]
                  }
               },
               "@code":"Yekaterina Lobaznyuk.2000",
               "@timestamp":1406852407913,
               "@when":1406852294506,
               "@metaKey":"FiPn-73iS_-sOiBgQOVxtg",
               "@what":{
                  "Year":"2000",
                  "Closing Ceremony Date":"10/1/00",
                  "Age":"17",
                  "Athlete":"Yekaterina Lobaznyuk",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"1",
                  "Sport":"Gymnastics",
                  "Country":"Russia",
                  "Silver Medals":"2"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Yekaterina Lobaznyuk.2000",
               "@whenCreated":1406852403653,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Yelena Zamolodchikova.2000",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Gymnastics"
                     ],
                     "code":[
                        "gymnastics"
                     ],
                     "props":{

                     },
                     "key":[
                        "gymnastics"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "17"
                     ]
                  },
                  "won":{
                     "name":[
                        "Yelena Zamolodchikova"
                     ],
                     "code":[
                        "yelena zamolodchikova"
                     ],
                     "props":{

                     },
                     "key":[
                        "yelenazamolodchikova"
                     ]
                  },
                  "2000":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "country":{
                     "name":[
                        "Russia"
                     ],
                     "code":[
                        "ru"
                     ],
                     "props":{

                     },
                     "key":[
                        "ru"
                     ]
                  }
               },
               "@code":"Yelena Zamolodchikova.2000",
               "@timestamp":1406852408025,
               "@when":1406852295525,
               "@metaKey":"3gRpnd0kSEaSD0-oZRw_og",
               "@what":{
                  "Year":"2000",
                  "Closing Ceremony Date":"10/1/00",
                  "Age":"17",
                  "Athlete":"Yelena Zamolodchikova",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Gymnastics",
                  "Country":"Russia",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Yelena Zamolodchikova.2000",
               "@whenCreated":1406852403668,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Chris Hoy.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2008":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Cycling"
                     ],
                     "code":[
                        "cycling"
                     ],
                     "props":{

                     },
                     "key":[
                        "cycling"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "32"
                     ]
                  },
                  "won":{
                     "name":[
                        "Chris Hoy"
                     ],
                     "code":[
                        "chris hoy"
                     ],
                     "props":{

                     },
                     "key":[
                        "chrishoy"
                     ]
                  },
                  "country":{
                     "name":[
                        "United Kingdom"
                     ],
                     "code":[
                        "gb"
                     ],
                     "props":{

                     },
                     "key":[
                        "gb"
                     ]
                  }
               },
               "@code":"Chris Hoy.2008",
               "@timestamp":1406852408219,
               "@when":1406852297025,
               "@metaKey":"g0zGOdUJSAqbg0Q3cech4w",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"32",
                  "Athlete":"Chris Hoy",
                  "Total Medals":"3",
                  "Gold Medals":"3",
                  "Bronze Medals":"0",
                  "Sport":"Cycling",
                  "Country":"United Kingdom",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Chris Hoy.2008",
               "@whenCreated":1406852403708,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Antje Buschschulte.2004",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "25"
                     ]
                  },
                  "won":{
                     "name":[
                        "Antje Buschschulte"
                     ],
                     "code":[
                        "antje buschschulte"
                     ],
                     "props":{

                     },
                     "key":[
                        "antjebuschschulte"
                     ]
                  },
                  "country":{
                     "name":[
                        "Germany"
                     ],
                     "code":[
                        "de"
                     ],
                     "props":{

                     },
                     "key":[
                        "de"
                     ]
                  }
               },
               "@code":"Antje Buschschulte.2004",
               "@timestamp":1406852335958,
               "@when":1406852233896,
               "@metaKey":"4XTXZlrsQWyPxuFg0GzIIg",
               "@what":{
                  "Year":"2004",
                  "Closing Ceremony Date":"8/29/04",
                  "Age":"25",
                  "Athlete":"Antje Buschschulte",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"3",
                  "Sport":"Swimming",
                  "Country":"Germany",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Antje Buschschulte.2004",
               "@whenCreated":1406852320813,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Georg Hettich.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2006":{
                     "name":[
                        "Silver Medals",
                        "Bronze Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "silver medals",
                        "bronze medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "silvermedals",
                        "bronzemedals",
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Nordic Combined"
                     ],
                     "code":[
                        "nordic combined"
                     ],
                     "props":{

                     },
                     "key":[
                        "nordiccombined"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "27"
                     ]
                  },
                  "won":{
                     "name":[
                        "Georg Hettich"
                     ],
                     "code":[
                        "georg hettich"
                     ],
                     "props":{

                     },
                     "key":[
                        "georghettich"
                     ]
                  },
                  "country":{
                     "name":[
                        "Germany"
                     ],
                     "code":[
                        "de"
                     ],
                     "props":{

                     },
                     "key":[
                        "de"
                     ]
                  }
               },
               "@code":"Georg Hettich.2006",
               "@timestamp":1406852406699,
               "@when":1406852282568,
               "@metaKey":"Hk6lFi6fTy6PsYzLC0Z-fw",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"27",
                  "Athlete":"Georg Hettich",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"1",
                  "Sport":"Nordic Combined",
                  "Country":"Germany",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Georg Hettich.2006",
               "@whenCreated":1406852403280,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Marian Dragulescu.2004",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Gymnastics"
                     ],
                     "code":[
                        "gymnastics"
                     ],
                     "props":{

                     },
                     "key":[
                        "gymnastics"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "23"
                     ]
                  },
                  "won":{
                     "name":[
                        "Marian Dragulescu"
                     ],
                     "code":[
                        "marian dragulescu"
                     ],
                     "props":{

                     },
                     "key":[
                        "mariandragulescu"
                     ]
                  },
                  "country":{
                     "name":[
                        "Romania"
                     ],
                     "code":[
                        "ro"
                     ],
                     "props":{

                     },
                     "key":[
                        "ro"
                     ]
                  }
               },
               "@code":"Marian Dragulescu.2004",
               "@timestamp":1406852407493,
               "@when":1406852290023,
               "@metaKey":"u0JvblcDRwSsTNt6Ud4yPA",
               "@what":{
                  "Year":"2004",
                  "Closing Ceremony Date":"8/29/04",
                  "Age":"23",
                  "Athlete":"Marian Dragulescu",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"2",
                  "Sport":"Gymnastics",
                  "Country":"Romania",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Marian Dragulescu.2004",
               "@whenCreated":1406852403537,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Catalina Ponor.2004",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Gymnastics"
                     ],
                     "code":[
                        "gymnastics"
                     ],
                     "props":{

                     },
                     "key":[
                        "gymnastics"
                     ]
                  },
                  "2004":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "16"
                     ]
                  },
                  "won":{
                     "name":[
                        "Catalina Ponor"
                     ],
                     "code":[
                        "catalina ponor"
                     ],
                     "props":{

                     },
                     "key":[
                        "catalinaponor"
                     ]
                  },
                  "country":{
                     "name":[
                        "Romania"
                     ],
                     "code":[
                        "ro"
                     ],
                     "props":{

                     },
                     "key":[
                        "ro"
                     ]
                  }
               },
               "@code":"Catalina Ponor.2004",
               "@timestamp":1406852407702,
               "@when":1406852292278,
               "@metaKey":"sqjj5_0bS8SQeYIDfTXgdg",
               "@what":{
                  "Year":"2004",
                  "Closing Ceremony Date":"8/29/04",
                  "Age":"16",
                  "Athlete":"Catalina Ponor",
                  "Total Medals":"3",
                  "Gold Medals":"3",
                  "Bronze Medals":"0",
                  "Sport":"Gymnastics",
                  "Country":"Romania",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Catalina Ponor.2004",
               "@whenCreated":1406852403602,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Justyna Kowalczyk.2010",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Cross Country Skiing"
                     ],
                     "code":[
                        "cross country skiing"
                     ],
                     "props":{

                     },
                     "key":[
                        "crosscountryskiing"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "27"
                     ]
                  },
                  "2010":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals",
                        "Bronze Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals",
                        "bronze medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals",
                        "bronzemedals"
                     ]
                  },
                  "won":{
                     "name":[
                        "Justyna Kowalczyk"
                     ],
                     "code":[
                        "justyna kowalczyk"
                     ],
                     "props":{

                     },
                     "key":[
                        "justynakowalczyk"
                     ]
                  },
                  "country":{
                     "name":[
                        "Poland"
                     ],
                     "code":[
                        "pl"
                     ],
                     "props":{

                     },
                     "key":[
                        "pl"
                     ]
                  }
               },
               "@code":"Justyna Kowalczyk.2010",
               "@timestamp":1406852408371,
               "@when":1406852299247,
               "@metaKey":"fJ_A1WjtRe-pcZgJKKwUBQ",
               "@what":{
                  "Year":"2010",
                  "Closing Ceremony Date":"2/28/10",
                  "Age":"27",
                  "Athlete":"Justyna Kowalczyk",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"1",
                  "Sport":"Cross Country Skiing",
                  "Country":"Poland",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Justyna Kowalczyk.2010",
               "@whenCreated":1406852403780,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Johan Olsson.2010",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Cross Country Skiing"
                     ],
                     "code":[
                        "cross country skiing"
                     ],
                     "props":{

                     },
                     "key":[
                        "crosscountryskiing"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "29"
                     ]
                  },
                  "2010":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "won":{
                     "name":[
                        "Johan Olsson"
                     ],
                     "code":[
                        "johan olsson"
                     ],
                     "props":{

                     },
                     "key":[
                        "johanolsson"
                     ]
                  },
                  "country":{
                     "name":[
                        "Sweden"
                     ],
                     "code":[
                        "se"
                     ],
                     "props":{

                     },
                     "key":[
                        "se"
                     ]
                  }
               },
               "@code":"Johan Olsson.2010",
               "@timestamp":1406852408476,
               "@when":1406852299950,
               "@metaKey":"V72AqyDISSWAsn8U-wgXnw",
               "@what":{
                  "Year":"2010",
                  "Closing Ceremony Date":"2/28/10",
                  "Age":"29",
                  "Athlete":"Johan Olsson",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"2",
                  "Sport":"Cross Country Skiing",
                  "Country":"Sweden",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Johan Olsson.2010",
               "@whenCreated":1406852403817,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Wang Meng.2010",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Short-Track Speed Skating"
                     ],
                     "code":[
                        "short-track speed skating"
                     ],
                     "props":{

                     },
                     "key":[
                        "short-trackspeedskating"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "24"
                     ]
                  },
                  "2010":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "won":{
                     "name":[
                        "Wang Meng"
                     ],
                     "code":[
                        "wang meng"
                     ],
                     "props":{

                     },
                     "key":[
                        "wangmeng"
                     ]
                  },
                  "country":{
                     "name":[
                        "China"
                     ],
                     "code":[
                        "cn"
                     ],
                     "props":{

                     },
                     "key":[
                        "cn"
                     ]
                  }
               },
               "@code":"Wang Meng.2010",
               "@timestamp":1406852339457,
               "@when":1406852251354,
               "@metaKey":"mh-z_mg-S3GZrGupQjZ1YQ",
               "@what":{
                  "Year":"2010",
                  "Closing Ceremony Date":"2/28/10",
                  "Age":"24",
                  "Athlete":"Wang Meng",
                  "Total Medals":"3",
                  "Gold Medals":"3",
                  "Bronze Medals":"0",
                  "Sport":"Short-Track Speed Skating",
                  "Country":"China",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Wang Meng.2010",
               "@whenCreated":1406852321713,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Apolo Anton Ohno.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2006":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Short-Track Speed Skating"
                     ],
                     "code":[
                        "short-track speed skating"
                     ],
                     "props":{

                     },
                     "key":[
                        "short-trackspeedskating"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "23"
                     ]
                  },
                  "won":{
                     "name":[
                        "Apolo Anton Ohno"
                     ],
                     "code":[
                        "apolo anton ohno"
                     ],
                     "props":{

                     },
                     "key":[
                        "apoloantonohno"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Apolo Anton Ohno.2006",
               "@timestamp":1406852339911,
               "@when":1406852253593,
               "@metaKey":"o4uJtEXaSi2_-sM7eqz8sQ",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"23",
                  "Athlete":"Apolo Anton Ohno",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"2",
                  "Sport":"Short-Track Speed Skating",
                  "Country":"United States",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Apolo Anton Ohno.2006",
               "@whenCreated":1406852321884,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Yang Yang (A).2002",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Short-Track Speed Skating"
                     ],
                     "code":[
                        "short-track speed skating"
                     ],
                     "props":{

                     },
                     "key":[
                        "short-trackspeedskating"
                     ]
                  },
                  "2002":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "25"
                     ]
                  },
                  "won":{
                     "name":[
                        "Yang Yang (A)"
                     ],
                     "code":[
                        "yang yang (a)"
                     ],
                     "props":{

                     },
                     "key":[
                        "yangyang(a)"
                     ]
                  },
                  "country":{
                     "name":[
                        "China"
                     ],
                     "code":[
                        "cn"
                     ],
                     "props":{

                     },
                     "key":[
                        "cn"
                     ]
                  }
               },
               "@code":"Yang Yang (A).2002",
               "@timestamp":1406852340386,
               "@when":1406852255786,
               "@metaKey":"enBD3oi2QE6bcxl1fY2JwA",
               "@what":{
                  "Year":"2002",
                  "Closing Ceremony Date":"2/24/02",
                  "Age":"25",
                  "Athlete":"Yang Yang (A)",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Short-Track Speed Skating",
                  "Country":"China",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Yang Yang (A).2002",
               "@whenCreated":1406852321999,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Chad Hedrick.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2006":{
                     "name":[
                        "Bronze Medals",
                        "Silver Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "bronze medals",
                        "silver medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "bronzemedals",
                        "silvermedals",
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Speed Skating"
                     ],
                     "code":[
                        "speed skating"
                     ],
                     "props":{

                     },
                     "key":[
                        "speedskating"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "28"
                     ]
                  },
                  "won":{
                     "name":[
                        "Chad Hedrick"
                     ],
                     "code":[
                        "chad hedrick"
                     ],
                     "props":{

                     },
                     "key":[
                        "chadhedrick"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Chad Hedrick.2006",
               "@timestamp":1406852341199,
               "@when":1406852258670,
               "@metaKey":"GjYSN_9XRLuXUJ7gPLGDoQ",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"28",
                  "Athlete":"Chad Hedrick",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"1",
                  "Sport":"Speed Skating",
                  "Country":"United States",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Chad Hedrick.2006",
               "@whenCreated":1406852322200,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Dara Torres.2000",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "33"
                     ]
                  },
                  "won":{
                     "name":[
                        "Dara Torres"
                     ],
                     "code":[
                        "dara torres"
                     ],
                     "props":{

                     },
                     "key":[
                        "daratorres"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  },
                  "2000":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  }
               },
               "@code":"Dara Torres.2000",
               "@timestamp":1406852328812,
               "@when":1406852191173,
               "@metaKey":"CRa0oHGzRAa7V1fBhErtVQ",
               "@what":{
                  "Year":"2000",
                  "Closing Ceremony Date":"10/1/00",
                  "Age":"33",
                  "Athlete":"Dara Torres",
                  "Total Medals":"5",
                  "Gold Medals":"2",
                  "Bronze Medals":"3",
                  "Sport":"Swimming",
                  "Country":"United States",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Dara Torres.2000",
               "@whenCreated":1406852318535,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Carly Patterson.2004",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Gymnastics"
                     ],
                     "code":[
                        "gymnastics"
                     ],
                     "props":{

                     },
                     "key":[
                        "gymnastics"
                     ]
                  },
                  "2004":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "16"
                     ]
                  },
                  "won":{
                     "name":[
                        "Carly Patterson"
                     ],
                     "code":[
                        "carly patterson"
                     ],
                     "props":{

                     },
                     "key":[
                        "carlypatterson"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Carly Patterson.2004",
               "@timestamp":1406852407657,
               "@when":1406852291561,
               "@metaKey":"UAIH441CTyiBpAZ-no4GZA",
               "@what":{
                  "Year":"2004",
                  "Closing Ceremony Date":"8/29/04",
                  "Age":"16",
                  "Athlete":"Carly Patterson",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"0",
                  "Sport":"Gymnastics",
                  "Country":"United States",
                  "Silver Medals":"2"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Carly Patterson.2004",
               "@whenCreated":1406852403578,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Guo Shuang.2012",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Cycling"
                     ],
                     "code":[
                        "cycling"
                     ],
                     "props":{

                     },
                     "key":[
                        "cycling"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "26"
                     ]
                  },
                  "won":{
                     "name":[
                        "Guo Shuang"
                     ],
                     "code":[
                        "guo shuang"
                     ],
                     "props":{

                     },
                     "key":[
                        "guoshuang"
                     ]
                  },
                  "country":{
                     "name":[
                        "China"
                     ],
                     "code":[
                        "cn"
                     ],
                     "props":{

                     },
                     "key":[
                        "cn"
                     ]
                  }
               },
               "@code":"Guo Shuang.2012",
               "@timestamp":1406852408148,
               "@when":1406852296271,
               "@metaKey":"3jQU5TuLR2qia-1c8NxV-A",
               "@what":{
                  "Year":"2012",
                  "Closing Ceremony Date":"8/12/12",
                  "Age":"26",
                  "Athlete":"Guo Shuang",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"1",
                  "Sport":"Cycling",
                  "Country":"China",
                  "Silver Medals":"2"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Guo Shuang.2012",
               "@whenCreated":1406852403675,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Magdalena Neuner.2010",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Biathlon"
                     ],
                     "code":[
                        "biathlon"
                     ],
                     "props":{

                     },
                     "key":[
                        "biathlon"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "23"
                     ]
                  },
                  "2010":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "won":{
                     "name":[
                        "Magdalena Neuner"
                     ],
                     "code":[
                        "magdalena neuner"
                     ],
                     "props":{

                     },
                     "key":[
                        "magdalenaneuner"
                     ]
                  },
                  "country":{
                     "name":[
                        "Germany"
                     ],
                     "code":[
                        "de"
                     ],
                     "props":{

                     },
                     "key":[
                        "de"
                     ]
                  }
               },
               "@code":"Magdalena Neuner.2010",
               "@timestamp":1406852408822,
               "@when":1406852304146,
               "@metaKey":"Ira2X9XASxi1TqQZl_f9aQ",
               "@what":{
                  "Year":"2010",
                  "Closing Ceremony Date":"2/28/10",
                  "Age":"23",
                  "Athlete":"Magdalena Neuner",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Biathlon",
                  "Country":"Germany",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Magdalena Neuner.2010",
               "@whenCreated":1406852403926,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Sven Fischer.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2006":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Biathlon"
                     ],
                     "code":[
                        "biathlon"
                     ],
                     "props":{

                     },
                     "key":[
                        "biathlon"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "34"
                     ]
                  },
                  "won":{
                     "name":[
                        "Sven Fischer"
                     ],
                     "code":[
                        "sven fischer"
                     ],
                     "props":{

                     },
                     "key":[
                        "svenfischer"
                     ]
                  },
                  "country":{
                     "name":[
                        "Germany"
                     ],
                     "code":[
                        "de"
                     ],
                     "props":{

                     },
                     "key":[
                        "de"
                     ]
                  }
               },
               "@code":"Sven Fischer.2006",
               "@timestamp":1406852409355,
               "@when":1406852307172,
               "@metaKey":"k3e-EZYXRU26QUVM6UQvDA",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"34",
                  "Athlete":"Sven Fischer",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"1",
                  "Sport":"Biathlon",
                  "Country":"Germany",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Sven Fischer.2006",
               "@whenCreated":1406852404144,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Kati Wilhelm.2002",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Biathlon"
                     ],
                     "code":[
                        "biathlon"
                     ],
                     "props":{

                     },
                     "key":[
                        "biathlon"
                     ]
                  },
                  "2002":{
                     "name":[
                        "Silver Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "silver medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "silvermedals",
                        "goldmedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "25"
                     ]
                  },
                  "won":{
                     "name":[
                        "Kati Wilhelm"
                     ],
                     "code":[
                        "kati wilhelm"
                     ],
                     "props":{

                     },
                     "key":[
                        "katiwilhelm"
                     ]
                  },
                  "country":{
                     "name":[
                        "Germany"
                     ],
                     "code":[
                        "de"
                     ],
                     "props":{

                     },
                     "key":[
                        "de"
                     ]
                  }
               },
               "@code":"Kati Wilhelm.2002",
               "@timestamp":1406852409767,
               "@when":1406852310471,
               "@metaKey":"-42u1POwRUmm1q80IFE_ZA",
               "@what":{
                  "Year":"2002",
                  "Closing Ceremony Date":"2/24/02",
                  "Age":"25",
                  "Athlete":"Kati Wilhelm",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Biathlon",
                  "Country":"Germany",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Kati Wilhelm.2002",
               "@whenCreated":1406852404273,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Michael Phelps.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2008":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "23"
                     ]
                  },
                  "won":{
                     "name":[
                        "Michael Phelps"
                     ],
                     "code":[
                        "michael phelps"
                     ],
                     "props":{

                     },
                     "key":[
                        "michaelphelps"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Michael Phelps.2008",
               "@timestamp":1406852323468,
               "@when":1406852182091,
               "@metaKey":"IU8mfjhYRJWwtfr6aTXPFQ",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"23",
                  "Athlete":"Michael Phelps",
                  "Total Medals":"8",
                  "Gold Medals":"8",
                  "Bronze Medals":"0",
                  "Sport":"Swimming",
                  "Country":"United States",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Michael Phelps.2008",
               "@whenCreated":1406852317688,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Ryan Lochte.2012",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "2012":{
                     "name":[
                        "Gold Medals",
                        "Bronze Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "bronze medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "bronzemedals",
                        "silvermedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "27"
                     ]
                  },
                  "won":{
                     "name":[
                        "Ryan Lochte"
                     ],
                     "code":[
                        "ryan lochte"
                     ],
                     "props":{

                     },
                     "key":[
                        "ryanlochte"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Ryan Lochte.2012",
               "@timestamp":1406852326286,
               "@when":1406852187852,
               "@metaKey":"EtEAnHzRTpezyADq9NkXBg",
               "@what":{
                  "Year":"2012",
                  "Closing Ceremony Date":"8/12/12",
                  "Age":"27",
                  "Athlete":"Ryan Lochte",
                  "Total Medals":"5",
                  "Gold Medals":"2",
                  "Bronze Medals":"1",
                  "Sport":"Swimming",
                  "Country":"United States",
                  "Silver Medals":"2"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Ryan Lochte.2012",
               "@whenCreated":1406852318363,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Natalie Coughlin.2004",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Swimming"
                     ],
                     "code":[
                        "swimming"
                     ],
                     "props":{

                     },
                     "key":[
                        "swimming"
                     ]
                  },
                  "2004":{
                     "name":[
                        "Bronze Medals",
                        "Silver Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "bronze medals",
                        "silver medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "bronzemedals",
                        "silvermedals",
                        "goldmedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "21"
                     ]
                  },
                  "won":{
                     "name":[
                        "Natalie Coughlin"
                     ],
                     "code":[
                        "natalie coughlin"
                     ],
                     "props":{

                     },
                     "key":[
                        "nataliecoughlin"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Natalie Coughlin.2004",
               "@timestamp":1406852327304,
               "@when":1406852189535,
               "@metaKey":"rudiQ4oSQyCZL37HvFXA7g",
               "@what":{
                  "Year":"2004",
                  "Closing Ceremony Date":"8/29/04",
                  "Age":"21",
                  "Athlete":"Natalie Coughlin",
                  "Total Medals":"5",
                  "Gold Medals":"2",
                  "Bronze Medals":"1",
                  "Sport":"Swimming",
                  "Country":"United States",
                  "Silver Medals":"2"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Natalie Coughlin.2004",
               "@whenCreated":1406852318463,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Cindy Klassen.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2006":{
                     "name":[
                        "Bronze Medals",
                        "Silver Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "bronze medals",
                        "silver medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "bronzemedals",
                        "silvermedals",
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Speed Skating"
                     ],
                     "code":[
                        "speed skating"
                     ],
                     "props":{

                     },
                     "key":[
                        "speedskating"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "26"
                     ]
                  },
                  "won":{
                     "name":[
                        "Cindy Klassen"
                     ],
                     "code":[
                        "cindy klassen"
                     ],
                     "props":{

                     },
                     "key":[
                        "cindyklassen"
                     ]
                  },
                  "country":{
                     "name":[
                        "Canada"
                     ],
                     "code":[
                        "ca"
                     ],
                     "props":{

                     },
                     "key":[
                        "ca"
                     ]
                  }
               },
               "@code":"Cindy Klassen.2006",
               "@timestamp":1406852329120,
               "@when":1406852191926,
               "@metaKey":"CaJpxAduQHyM9gQHMa9b4A",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"26",
                  "Athlete":"Cindy Klassen",
                  "Total Medals":"5",
                  "Gold Medals":"1",
                  "Bronze Medals":"2",
                  "Sport":"Speed Skating",
                  "Country":"Canada",
                  "Silver Medals":"2"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Cindy Klassen.2006",
               "@whenCreated":1406852318556,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Ole Einar Bjørndalen.2002",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Biathlon"
                     ],
                     "code":[
                        "biathlon"
                     ],
                     "props":{

                     },
                     "key":[
                        "biathlon"
                     ]
                  },
                  "2002":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "28"
                     ]
                  },
                  "won":{
                     "name":[
                        "Ole Einar Bjørndalen"
                     ],
                     "code":[
                        "ole einar bjørndalen"
                     ],
                     "props":{

                     },
                     "key":[
                        "oleeinarbjørndalen"
                     ]
                  },
                  "country":{
                     "name":[
                        "Norway"
                     ],
                     "code":[
                        "no"
                     ],
                     "props":{

                     },
                     "key":[
                        "no"
                     ]
                  }
               },
               "@code":"Ole Einar Bjørndalen.2002",
               "@timestamp":1406852332093,
               "@when":1406852210188,
               "@metaKey":"KVGfjCerQgCNly5ZJj6RSg",
               "@what":{
                  "Year":"2002",
                  "Closing Ceremony Date":"2/24/02",
                  "Age":"28",
                  "Athlete":"Ole Einar Bjørndalen",
                  "Total Medals":"4",
                  "Gold Medals":"4",
                  "Bronze Medals":"0",
                  "Sport":"Biathlon",
                  "Country":"Norway",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Ole Einar Bjørndalen.2002",
               "@whenCreated":1406852319721,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Bradley Wiggins.2004",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Cycling"
                     ],
                     "code":[
                        "cycling"
                     ],
                     "props":{

                     },
                     "key":[
                        "cycling"
                     ]
                  },
                  "2004":{
                     "name":[
                        "Bronze Medals",
                        "Silver Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "bronze medals",
                        "silver medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "bronzemedals",
                        "silvermedals",
                        "goldmedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "24"
                     ]
                  },
                  "won":{
                     "name":[
                        "Bradley Wiggins"
                     ],
                     "code":[
                        "bradley wiggins"
                     ],
                     "props":{

                     },
                     "key":[
                        "bradleywiggins"
                     ]
                  },
                  "country":{
                     "name":[
                        "United Kingdom"
                     ],
                     "code":[
                        "gb"
                     ],
                     "props":{

                     },
                     "key":[
                        "gb"
                     ]
                  }
               },
               "@code":"Bradley Wiggins.2004",
               "@timestamp":1406852408292,
               "@when":1406852297758,
               "@metaKey":"LxNzqQiCRICZw6dNv9RYGw",
               "@what":{
                  "Year":"2004",
                  "Closing Ceremony Date":"8/29/04",
                  "Age":"24",
                  "Athlete":"Bradley Wiggins",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"1",
                  "Sport":"Cycling",
                  "Country":"United Kingdom",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Bradley Wiggins.2004",
               "@whenCreated":1406852403742,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Stefania Belmondo.2002",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Cross Country Skiing"
                     ],
                     "code":[
                        "cross country skiing"
                     ],
                     "props":{

                     },
                     "key":[
                        "crosscountryskiing"
                     ]
                  },
                  "2002":{
                     "name":[
                        "Bronze Medals",
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "bronze medals",
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "bronzemedals",
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "33"
                     ]
                  },
                  "won":{
                     "name":[
                        "Stefania Belmondo"
                     ],
                     "code":[
                        "stefania belmondo"
                     ],
                     "props":{

                     },
                     "key":[
                        "stefaniabelmondo"
                     ]
                  },
                  "country":{
                     "name":[
                        "Italy"
                     ],
                     "code":[
                        "it"
                     ],
                     "props":{

                     },
                     "key":[
                        "it"
                     ]
                  }
               },
               "@code":"Stefania Belmondo.2002",
               "@timestamp":1406852408542,
               "@when":1406852300687,
               "@metaKey":"LFg1ydcEQ6e_NN2O1n7gsw",
               "@what":{
                  "Year":"2002",
                  "Closing Ceremony Date":"2/24/02",
                  "Age":"33",
                  "Athlete":"Stefania Belmondo",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"1",
                  "Sport":"Cross Country Skiing",
                  "Country":"Italy",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Stefania Belmondo.2002",
               "@whenCreated":1406852403833,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Yuliya Chepalova.2002",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Cross Country Skiing"
                     ],
                     "code":[
                        "cross country skiing"
                     ],
                     "props":{

                     },
                     "key":[
                        "crosscountryskiing"
                     ]
                  },
                  "2002":{
                     "name":[
                        "Bronze Medals",
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "bronze medals",
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "bronzemedals",
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "25"
                     ]
                  },
                  "won":{
                     "name":[
                        "Yuliya Chepalova"
                     ],
                     "code":[
                        "yuliya chepalova"
                     ],
                     "props":{

                     },
                     "key":[
                        "yuliyachepalova"
                     ]
                  },
                  "country":{
                     "name":[
                        "Russia"
                     ],
                     "code":[
                        "ru"
                     ],
                     "props":{

                     },
                     "key":[
                        "ru"
                     ]
                  }
               },
               "@code":"Yuliya Chepalova.2002",
               "@timestamp":1406852408593,
               "@when":1406852301452,
               "@metaKey":"Q2vdX966RP6twY8fSmITRQ",
               "@what":{
                  "Year":"2002",
                  "Closing Ceremony Date":"2/24/02",
                  "Age":"25",
                  "Athlete":"Yuliya Chepalova",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"1",
                  "Sport":"Cross Country Skiing",
                  "Country":"Russia",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Yuliya Chepalova.2002",
               "@whenCreated":1406852403849,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Frode Estil.2002",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Cross Country Skiing"
                     ],
                     "code":[
                        "cross country skiing"
                     ],
                     "props":{

                     },
                     "key":[
                        "crosscountryskiing"
                     ]
                  },
                  "2002":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "29"
                     ]
                  },
                  "won":{
                     "name":[
                        "Frode Estil"
                     ],
                     "code":[
                        "frode estil"
                     ],
                     "props":{

                     },
                     "key":[
                        "frodeestil"
                     ]
                  },
                  "country":{
                     "name":[
                        "Norway"
                     ],
                     "code":[
                        "no"
                     ],
                     "props":{

                     },
                     "key":[
                        "no"
                     ]
                  }
               },
               "@code":"Frode Estil.2002",
               "@timestamp":1406852408628,
               "@when":1406852302575,
               "@metaKey":"SNxlU4qeQOie1PiksopdXA",
               "@what":{
                  "Year":"2002",
                  "Closing Ceremony Date":"2/24/02",
                  "Age":"29",
                  "Athlete":"Frode Estil",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Cross Country Skiing",
                  "Country":"Norway",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Frode Estil.2002",
               "@whenCreated":1406852403881,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Emil Hegle Svendsen.2010",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Biathlon"
                     ],
                     "code":[
                        "biathlon"
                     ],
                     "props":{

                     },
                     "key":[
                        "biathlon"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "24"
                     ]
                  },
                  "2010":{
                     "name":[
                        "Gold Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "silvermedals"
                     ]
                  },
                  "won":{
                     "name":[
                        "Emil Hegle Svendsen"
                     ],
                     "code":[
                        "emil hegle svendsen"
                     ],
                     "props":{

                     },
                     "key":[
                        "emilheglesvendsen"
                     ]
                  },
                  "country":{
                     "name":[
                        "Norway"
                     ],
                     "code":[
                        "no"
                     ],
                     "props":{

                     },
                     "key":[
                        "no"
                     ]
                  }
               },
               "@code":"Emil Hegle Svendsen.2010",
               "@timestamp":1406852408952,
               "@when":1406852304875,
               "@metaKey":"Cp6Kj2RCT4y7y0672ismfA",
               "@what":{
                  "Year":"2010",
                  "Closing Ceremony Date":"2/28/10",
                  "Age":"24",
                  "Athlete":"Emil Hegle Svendsen",
                  "Total Medals":"3",
                  "Gold Medals":"2",
                  "Bronze Medals":"0",
                  "Sport":"Biathlon",
                  "Country":"Norway",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Emil Hegle Svendsen.2010",
               "@whenCreated":1406852403966,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Martina Glagow-Beck.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Biathlon"
                     ],
                     "code":[
                        "biathlon"
                     ],
                     "props":{

                     },
                     "key":[
                        "biathlon"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "26"
                     ]
                  },
                  "won":{
                     "name":[
                        "Martina Glagow-Beck"
                     ],
                     "code":[
                        "martina glagow-beck"
                     ],
                     "props":{

                     },
                     "key":[
                        "martinaglagow-beck"
                     ]
                  },
                  "country":{
                     "name":[
                        "Germany"
                     ],
                     "code":[
                        "de"
                     ],
                     "props":{

                     },
                     "key":[
                        "de"
                     ]
                  }
               },
               "@code":"Martina Glagow-Beck.2006",
               "@timestamp":1406852409394,
               "@when":1406852307911,
               "@metaKey":"auIedUZeQ4a-JQXXs0EP1w",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"26",
                  "Athlete":"Martina Glagow-Beck",
                  "Total Medals":"3",
                  "Gold Medals":"0",
                  "Bronze Medals":"0",
                  "Sport":"Biathlon",
                  "Country":"Germany",
                  "Silver Medals":"3"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Martina Glagow-Beck.2006",
               "@whenCreated":1406852404174,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Yohan Blake.2012",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Athletics"
                     ],
                     "code":[
                        "athletics"
                     ],
                     "props":{

                     },
                     "key":[
                        "athletics"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "22"
                     ]
                  },
                  "2012":{
                     "name":[
                        "Silver Medals",
                        "Gold Medals"
                     ],
                     "code":[
                        "silver medals",
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "silvermedals",
                        "goldmedals"
                     ]
                  },
                  "won":{
                     "name":[
                        "Yohan Blake"
                     ],
                     "code":[
                        "yohan blake"
                     ],
                     "props":{

                     },
                     "key":[
                        "yohanblake"
                     ]
                  },
                  "country":{
                     "name":[
                        "Jamaica"
                     ],
                     "code":[
                        "jm"
                     ],
                     "props":{

                     },
                     "key":[
                        "jm"
                     ]
                  }
               },
               "@code":"Yohan Blake.2012",
               "@timestamp":1406852409902,
               "@when":1406852311429,
               "@metaKey":"i8rTLQ5iTqGuXmUKTyd3Fg",
               "@what":{
                  "Year":"2012",
                  "Closing Ceremony Date":"8/12/12",
                  "Age":"22",
                  "Athlete":"Yohan Blake",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"0",
                  "Sport":"Athletics",
                  "Country":"Jamaica",
                  "Silver Medals":"2"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Yohan Blake.2012",
               "@whenCreated":1406852404291,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Carmelita Jeter.2012",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "skilled":{
                     "name":[
                        "Athletics"
                     ],
                     "code":[
                        "athletics"
                     ],
                     "props":{

                     },
                     "key":[
                        "athletics"
                     ]
                  },
                  "2012":{
                     "name":[
                        "Gold Medals",
                        "Bronze Medals",
                        "Silver Medals"
                     ],
                     "code":[
                        "gold medals",
                        "bronze medals",
                        "silver medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals",
                        "bronzemedals",
                        "silvermedals"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "32"
                     ]
                  },
                  "won":{
                     "name":[
                        "Carmelita Jeter"
                     ],
                     "code":[
                        "carmelita jeter"
                     ],
                     "props":{

                     },
                     "key":[
                        "carmelitajeter"
                     ]
                  },
                  "country":{
                     "name":[
                        "United States"
                     ],
                     "code":[
                        "us"
                     ],
                     "props":{

                     },
                     "key":[
                        "us"
                     ]
                  }
               },
               "@code":"Carmelita Jeter.2012",
               "@timestamp":1406852410035,
               "@when":1406852314338,
               "@metaKey":"9aH-O8unSGC7ffnUqKNBpQ",
               "@what":{
                  "Year":"2012",
                  "Closing Ceremony Date":"8/12/12",
                  "Age":"32",
                  "Athlete":"Carmelita Jeter",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"1",
                  "Sport":"Athletics",
                  "Country":"United States",
                  "Silver Medals":"1"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Carmelita Jeter.2012",
               "@whenCreated":1406852404421,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Usain Bolt.2008",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2008":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Athletics"
                     ],
                     "code":[
                        "athletics"
                     ],
                     "props":{

                     },
                     "key":[
                        "athletics"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "21"
                     ]
                  },
                  "won":{
                     "name":[
                        "Usain Bolt"
                     ],
                     "code":[
                        "usain bolt"
                     ],
                     "props":{

                     },
                     "key":[
                        "usainbolt"
                     ]
                  },
                  "country":{
                     "name":[
                        "Jamaica"
                     ],
                     "code":[
                        "jm"
                     ],
                     "props":{

                     },
                     "key":[
                        "jm"
                     ]
                  }
               },
               "@code":"Usain Bolt.2008",
               "@timestamp":1406852410070,
               "@when":1406852315147,
               "@metaKey":"3GiFaVQEQO6oLOuaIDE2ag",
               "@what":{
                  "Year":"2008",
                  "Closing Ceremony Date":"8/24/08",
                  "Age":"21",
                  "Athlete":"Usain Bolt",
                  "Total Medals":"3",
                  "Gold Medals":"3",
                  "Bronze Medals":"0",
                  "Sport":"Athletics",
                  "Country":"Jamaica",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Usain Bolt.2008",
               "@whenCreated":1406852404440,
               "@who":"mike"
            }
         },
         {
            "_index":"ab.monowai.olympic",
            "_type":"competition",
            "_id":"Lars Bystøl.2006",
            "_score":0.0,
            "_source":{
               "@tag":{
                  "2006":{
                     "name":[
                        "Gold Medals"
                     ],
                     "code":[
                        "gold medals"
                     ],
                     "props":{

                     },
                     "key":[
                        "goldmedals"
                     ]
                  },
                  "skilled":{
                     "name":[
                        "Ski Jumping"
                     ],
                     "code":[
                        "ski jumping"
                     ],
                     "props":{

                     },
                     "key":[
                        "skijumping"
                     ]
                  },
                  "at":{
                     "props":{

                     },
                     "key":[
                        "27"
                     ]
                  },
                  "won":{
                     "name":[
                        "Lars Bystøl"
                     ],
                     "code":[
                        "lars bystøl"
                     ],
                     "props":{

                     },
                     "key":[
                        "larsbystøl"
                     ]
                  },
                  "country":{
                     "name":[
                        "Norway"
                     ],
                     "code":[
                        "no"
                     ],
                     "props":{

                     },
                     "key":[
                        "no"
                     ]
                  }
               },
               "@code":"Lars Bystøl.2006",
               "@timestamp":1406852406335,
               "@when":1406852280189,
               "@metaKey":"_Rt7wXaDSbayzm6fKRtcog",
               "@what":{
                  "Year":"2006",
                  "Closing Ceremony Date":"2/26/06",
                  "Age":"27",
                  "Athlete":"Lars Bystøl",
                  "Total Medals":"3",
                  "Gold Medals":"1",
                  "Bronze Medals":"2",
                  "Sport":"Ski Jumping",
                  "Country":"Norway",
                  "Silver Medals":"0"
               },
               "@fortress":"Olympic",
               "@docType":"competition",
               "@lastEvent":"create",
               "@description":"competition.Lars Bystøl.2006",
               "@whenCreated":1406852403223,
               "@who":"mike"
            }
         }
      ]
   }
}



"""


