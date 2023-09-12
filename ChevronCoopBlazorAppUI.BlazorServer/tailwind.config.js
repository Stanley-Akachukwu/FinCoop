module.exports = {
    content: [
        '**/*.{razor,cshtml,html}',
        './Pages/*.js',
        './Areas/*.js',
        './node_modules/flowbite/**/*.js',
        './node_modules/sortablejs/**/*.js'
    ],
    safelist: [
        'w-64',
        'w-1/2',
        'rounded-l-lg',
        'rounded-r-lg',
        'bg-gray-200',
        'grid-cols-4',
        'grid-cols-7',
        'h-6',
        'leading-6',
        'h-9',
        'leading-9',
        'shadow-lg',
        'bg-opacity-50',
        'dark:bg-opacity-80'
    ],
    darkMode: "class",
    theme: {
        extend: {
            maxWidth: {
                '1/4': '25%',
                '1/2': '50%',
                '2/5': '40%',
                '3/5': '60%',
                '1/6': '16.666667%'
            },
            backgroundImage: {
                "dashboard-top-bg": "linear-gradient(326.3deg, #06549B 18.26%, #2FA1D3 76.66%, #2FA1D3 113.82%)",
                'gradient-primary': 'linear-gradient(326.3deg, #06549B 18.26%, #2FA1D3 76.66%, #2FA1D3 113.82%)',
                'gradient-halfblue': 'linear-gradient(to bottom, #07579D 50% , white 50%)',
            },
            colors: {
                primary: { "50": "#eff6ff", "100": "#dbeafe", "200": "#bfdbfe", "300": "#93c5fd", "400": "#60a5fa", "500": "#3b82f6", "600": "#2563eb", "700": "#1d4ed8", "800": "#1e40af", "900": "#1e3a8a" },
                "sidebar-active": "rgba(31, 130, 189, 0.2)",
                "sidebar-icons-active": "#07579D",
                "CEMCS-Blue-100": "#1F82BD",
                "BLACK-Transparent-100": "#0000003d",
                halfblue: '#07579D',
                lightblue: '#1F82BD',
                greylite: '#4B5563',
                greylighter: '#9CA3AF',
            },

            fontFamily: {
                'sans': ['Inter', 'ui-sans-serif', 'system-ui', '-apple-system', 'system-ui', 'Segoe UI', 'Roboto', 'Helvetica Neue', 'Arial', 'Noto Sans', 'sans-serif', 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol', 'Noto Color Emoji'],
                'body': ['Inter', 'ui-sans-serif', 'system-ui', '-apple-system', 'system-ui', 'Segoe UI', 'Roboto', 'Helvetica Neue', 'Arial', 'Noto Sans', 'sans-serif', 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol', 'Noto Color Emoji'],
                'mono': ['ui-monospace', 'SFMono-Regular', 'Menlo', 'Monaco', 'Consolas', 'Liberation Mono', 'Courier New', 'monospace']
            },
            transitionProperty: {
                'width': 'width'
            },
            textDecoration: ['active'],
            minWidth: {
                'kanban': '28rem'
            },
            padding: {
                'cooperative-hori': '10px',
                'cooperative-vecti': '2px'
            },
            height: {
                'dashboard-top-height': "288px"
            }
        },
    },
    corePlugins: {
        /*preflight: false,*/
    },
    plugins: [
        require('flowbite/plugin')
    ],
}
