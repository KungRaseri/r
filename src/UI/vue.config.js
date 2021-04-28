// vue.config.js
module.exports = {
    publicPath: "/ui",
    outputDir: "openrp-client-ui",
    filenameHashing: false,
    parallel: true,
    transpileDependencies: [
        'vuetify'
    ]
}
