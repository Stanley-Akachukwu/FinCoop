const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");
const { SourceMapDevToolPlugin } = require("webpack");
const path = require('path');

module.exports = {
    //for dev environment
    // REMEMBER to comment for prod
    mode: 'development',
    devtool: 'source-map',
   //devtool: 'eval-cheap-module-source-map',

    //for prod env
    // remember to uncomment before deployment
    //mode: 'production',
    //devtool: 'nosources-source-map',

    entry: {
        app: path.resolve(__dirname, 'assets/js/flowbite-scripts.js')
    },
    output: {
        filename: '[name].bundle.js',
        path: path.resolve(__dirname, 'wwwroot/js/libs/flowbite'),
        //library: {
        //    type: "module",
        //},
    },
    
    //experiments: {
    //    outputModule: true
    //},
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, 'css-loader', 'postcss-loader'],
            },
            {
                test: /\.(png|jpg|gif|svg)$/,
                loader: 'file-loader',
                options: {
                    outputPath: 'dist/images/'
                }
            },
            {
                test: /\.(ttf|eot|svg|gif|woff|woff2)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                use: [{
                    loader: 'file-loader',
                }]
            },
        ],
    },
    resolve: {
        extensions: ['', '.js', '.jsx', '.css']
    },
    plugins: [
        new MiniCssExtractPlugin(),
        new SourceMapDevToolPlugin({
            filename: "[file].map"
        })
    ],
    optimization: {
        minimize: false,
        minimizer: [
            new CssMinimizerPlugin()
        ]
    },
};