module.exports = {
    plugins: [
        '@snowpack/plugin-postcss'
    ],

    buildOptions: {
        out: './wwwroot/',
        clean: true
    },

    mount: {
        'TypeScript': '/js',
        'Styles': '/css'
    },

    devOptions: {
        tailwindConfig: './tailwind.config.js',
    },
};