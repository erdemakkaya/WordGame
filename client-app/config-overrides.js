//const { override, fixBabelImports } = require("customize-cra");
const { override, fixBabelImports, addLessLoader } = require("customize-cra");

path = require("path"),
fs = require('fs'),
lessToJs = require('less-vars-to-js'),
customizeVars = lessToJs(fs.readFileSync(path.join(__dirname, './customize-vars.less'), 'utf8'));

module.exports = override(
  fixBabelImports("import", {
    libraryName: "antd",
    libraryDirectory: "es",
    //style: "css",
    style: true
  }),
  addLessLoader({
    javascriptEnabled: true,
    modifyVars: customizeVars
  })
);