/** @type {import('tailwindcss').Config}*/
const config = {
  content: [
    "./src/**/*.{html,js,svelte,ts}",
    "./node_modules/flowbite-svelte/**/*.{html,js,svelte,ts}",
  ],

  plugins: [
    require('flowbite/plugin')
  ],
  
  theme: {
    extend: {
      colors: {
        primary: {
					DEFAULT: '#605BFF',
					50: '#FFFFFF',
					100: '#E6E5FF',
					200: '#DCDBFF',
					300: '#C9C7FF',
					400: '#ABA8FF',
					500: '#9794FF',
					600: '#7E7AFF',
					700: '#605BFF',
					800: '#2A23FF',
					900: '#0700EA',
					950: '#0600CE'
				}
      }
    },
  }
};

module.exports = config;
