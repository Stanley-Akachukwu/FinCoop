﻿(() => {

    eval("const isSidebarExpanded = toggleSidebarEl => {\r\n    return toggleSidebarEl.getAttribute('aria-expanded') === 'true' ? true : false;\r\n}\r\n\r\nconst toggleSidebar = (sidebarEl, expand, setExpanded = false) => {\r\n    const bottomMenuEl = document.querySelector('[sidebar-bottom-menu]');\r\n    const mainContentEl = document.getElementById('main-content');\r\n    if (expand) {\r\n        sidebarEl.classList.add('lg:w-64');\r\n        sidebarEl.classList.remove('lg:w-16');\r\n        mainContentEl.classList.add('lg:ml-64');\r\n        mainContentEl.classList.remove('lg:ml-16');\r\n\r\n        document.querySelectorAll('#' + sidebarEl.getAttribute('id') + ' [sidebar-toggle-item]').forEach(sidebarToggleEl => {\r\n            sidebarToggleEl.classList.remove('lg:hidden');\r\n            sidebarToggleEl.classList.remove('lg:absolute');\r\n        });\r\n\r\n        // toggle multi level menu item initial and full text\r\n        document.querySelectorAll('#' + sidebar.getAttribute('id') + ' ul > li > ul > li > a').forEach(e => {\r\n            e.classList.add('pl-11');\r\n            e.classList.remove('px-4');\r\n            e.childNodes[0].classList.remove('hidden');\r\n            e.childNodes[1].classList.add('hidden');\r\n        });\r\n\r\n        bottomMenuEl.classList.remove('flex-col', 'space-y-4', 'p-2');\r\n        bottomMenuEl.classList.add('space-x-4', 'p-4');\r\n        setExpanded ? toggleSidebarEl.setAttribute('aria-expanded', 'true') : null;\r\n    } else {\r\n        sidebarEl.classList.remove('lg:w-64');\r\n        sidebarEl.classList.add('lg:w-16');\r\n        mainContentEl.classList.remove('lg:ml-64');\r\n        mainContentEl.classList.add('lg:ml-16');\r\n        document.querySelectorAll('#' + sidebarEl.getAttribute('id') + ' [sidebar-toggle-item]').forEach(sidebarToggleEl => {\r\n            sidebarToggleEl.classList.add('lg:hidden');\r\n            sidebarToggleEl.classList.add('lg:absolute');\r\n        });\r\n\r\n        // toggle multi level menu item initial and full text\r\n        document.querySelectorAll('#' + sidebar.getAttribute('id') + ' ul > li > ul > li > a').forEach(e => {\r\n            e.classList.remove('pl-11');\r\n            e.classList.add('px-4');\r\n            e.childNodes[0].classList.add('hidden');\r\n            e.childNodes[1].classList.remove('hidden');\r\n        });\r\n\r\n        bottomMenuEl.classList.add('flex-col', 'space-y-4', 'p-2');\r\n        bottomMenuEl.classList.remove('space-x-4', 'p-4');\r\n        setExpanded ? toggleSidebarEl.setAttribute('aria-expanded', 'false') : null;\r\n    }\r\n}\r\n\r\nconst toggleSidebarEl = document.getElementById('toggleSidebar');\r\nconst sidebar = document.getElementById('sidebar');\r\n\r\ndocument.querySelectorAll('#' + sidebar.getAttribute('id') + ' ul > li > ul > li > a').forEach(e => {\r\n    var fullText = e.textContent;\r\n    var firstLetter = fullText.substring(0, 1);\r\n\r\n    var fullTextEl = document.createElement('span');\r\n    var firstLetterEl = document.createElement('span');\r\n    firstLetterEl.classList.add('hidden');\r\n    fullTextEl.textContent = fullText;\r\n    firstLetterEl.textContent = firstLetter;\r\n\r\n    e.textContent = '';\r\n    e.appendChild(fullTextEl);\r\n    e.appendChild(firstLetterEl);\r\n});\r\n\r\n// initialize sidebar\r\nif (localStorage.getItem('sidebarExpanded') !== null) {\r\n    if (localStorage.getItem('sidebarExpanded') === 'true') {\r\n        toggleSidebar(sidebar, true, false);\r\n    } else {\r\n        toggleSidebar(sidebar, false, true);\r\n    }\r\n}\r\n\r\ntoggleSidebarEl.addEventListener('click', () => {\r\n    localStorage.setItem('sidebarExpanded', !isSidebarExpanded(toggleSidebarEl));\r\n    toggleSidebar(sidebar, !isSidebarExpanded(toggleSidebarEl), true);\r\n});\r\n\r\nsidebar.addEventListener('mouseenter', () => {\r\n    if (!isSidebarExpanded(toggleSidebarEl)) {\r\n        toggleSidebar(sidebar, true);\r\n    }\r\n});\r\n\r\nsidebar.addEventListener('mouseleave', () => {\r\n    if (!isSidebarExpanded(toggleSidebarEl)) {\r\n        toggleSidebar(sidebar, false);\r\n    }\r\n});\r\n\r\nconst toggleSidebarMobile = (sidebar, sidebarBackdrop, toggleSidebarMobileHamburger, toggleSidebarMobileClose) => {\r\n    sidebar.classList.toggle('hidden');\r\n    sidebarBackdrop.classList.toggle('hidden');\r\n    toggleSidebarMobileHamburger.classList.toggle('hidden');\r\n    toggleSidebarMobileClose.classList.toggle('hidden');\r\n}\r\n\r\nconst toggleSidebarMobileEl = document.getElementById('toggleSidebarMobile');\r\nconst sidebarBackdrop = document.getElementById('sidebarBackdrop');\r\nconst toggleSidebarMobileHamburger = document.getElementById('toggleSidebarMobileHamburger');\r\nconst toggleSidebarMobileClose = document.getElementById('toggleSidebarMobileClose');\r\nconst toggleSidebarMobileSearch = document.getElementById('toggleSidebarMobileSearch');\r\n\r\ntoggleSidebarMobileSearch.addEventListener('click', () => {\r\n    toggleSidebarMobile(sidebar, sidebarBackdrop, toggleSidebarMobileHamburger, toggleSidebarMobileClose);\r\n});\r\n\r\ntoggleSidebarMobileEl.addEventListener('click', () => {\r\n    toggleSidebarMobile(sidebar, sidebarBackdrop, toggleSidebarMobileHamburger, toggleSidebarMobileClose);\r\n});\r\n\r\nsidebarBackdrop.addEventListener('click', () => {\r\n    toggleSidebarMobile(sidebar, sidebarBackdrop, toggleSidebarMobileHamburger, toggleSidebarMobileClose);\r\n});\n\n//# sourceURL=webpack://chevron-template/./src/js/sidebar.js?");

    /***/
}),