namespace ElasticOps.Configuration
open FSharp.Configuration


type ElasticOpsConfig = YamlConfig<"..\SolutionItems\Config.yaml">


type IntellisenseConfig = YamlConfig<"..\SolutionItems\IntellisenseConfig.yaml">

module ConfigLoaders =
    let LoadIntellisenseConfig () =
        let config = new IntellisenseConfig()
        config.Load("IntellisenseConfig.yaml")
        config

    let LoadElasticOpsConfig () =
        let config = new IntellisenseConfig()
        config.Load("Config.yaml")
        config