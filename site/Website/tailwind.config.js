const colors = require('tailwindcss/colors')

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "**/*.razor",
    "**/*.razor.cs",
    "**/*.cshtml",
    "**/*.html"
  ],
  theme: {
    colors: {
      transparent: 'transparent',
      current: 'currentColor',
      black: colors.black,
      white: colors.white,
      primary: colors.blue,
      gray: colors.gray,
      background: colors.slate,
      success: colors.green,
      warning: colors.yellow,
      danger: colors.red,
      info: colors.blue
    },
    extend: {},
  },
  plugins: [],
}

