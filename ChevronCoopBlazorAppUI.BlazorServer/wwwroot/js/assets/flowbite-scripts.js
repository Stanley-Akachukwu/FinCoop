import '../css/tailwind-styles.css';
// import 'svgmap/dist/svgMap.min.css';

import 'flowbite/dist/flowbite.js';
import './sidebar';
import './charts';
import './dark-mode';

import {
    initAccordions,
    initCarousels,
    initCollapses,
    initDials,
    initDismisses,
    initDrawers,
    initDropdowns,
    initModals,
    initPopovers,
    initTabs,
    initTooltips
} from 'flowbite'


//export {
//    initAccordion,
//    initCarousel,
//    iinitCollapse,
//    initDial,
//    initDismiss,
//    initDrawer,
//    initDropdown,
//    initModal,
//    initPopover,
//    initTabs,
//    initTooltip
//};




document.addEventListener("DOMSubtreeModified", function () {



    //initAccordion();
    //initCarousel();
    //iinitCollapse();
    //initDial();
    //initDismiss();
    //initDrawer();
    //initDropdown();
    //initModal();
    //initPopover();
    //initTabs();
    //initTooltip();

});


document.addEventListener("DOMContentLoaded", function () {


    //alert("DOMContentLoaded");

    //initAccordion();
    //initCarousel();
    //iinitCollapse();
    //initDial();
    //initDismiss();
    //initDrawer();
    //initDropdown();
    //initModal();
    //initPopover();
    //initTabs();
    //initTooltip();



});


window.initFlowbiteJS = () => {

    //alert("window.initFlowbiteJS");

    initAccordions();
    initCarousels();
    initCollapses();
    initDials();
    initDismisses();
    initDrawers();
    initDropdowns();
    initModals();
    initPopovers();
    initTabs();
    initTooltips();


}





window.getDimensions = function () {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};



