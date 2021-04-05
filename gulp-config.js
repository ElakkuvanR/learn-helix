module.exports = function () {
  var instanceRoot = "C:\\inetpub\\wwwroot\\learnmecm.dev.local";
  var config = {
    websiteRoot: instanceRoot + "\\",
    sitecoreLibraries: instanceRoot + "\\bin",
    licensePath: instanceRoot + "\\App_Data\\license.xml",
    packageXmlBasePath: ".\\src\\Project\\Habitat\\code\\App_Data\\packages\\habitat.xml",
    packagePath: instanceRoot + "\\App_Data\\packages",
    solutionName: "Learn.Helix",
    buildConfiguration: "Debug",
    buildToolsVersion: '15.0',
    buildMaxCpuCount: 0,
    buildVerbosity: "minimal",
    buildPlatform: "Any CPU",
    publishPlatform: "AnyCpu",
    runCleanBuilds: false
  };
  return config;
}
