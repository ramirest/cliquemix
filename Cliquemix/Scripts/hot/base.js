$(document).ready(function() {
 
  $("#owl-demo").owlCarousel({
      autoPlay: true,
      navigation : true, // Show next and prev buttons
	  navigationText :["<div class='next visible-lg'></div>","<div class='prev visible-lg'></div>"],
      slideSpeed : 300,
	  pagination: true,
      paginationSpeed : 400,
      singleItem:true
       
  });
 
});

$(document).ready(function() {
 
  $("#galeria").owlCarousel({
 
      autoPlay: 3000, //Set AutoPlay to 3 seconds
	   navigation : true, // Show next and prev buttons
	  navigationText :["<div class='naveg-menor'></div>","<div class='naveg-maior'></div>"],
      pagination: true,
      items : 2,
      
 
  });
 
});


jQuery(window).resize(function () {
    'use strict';
    var windowWidth;
    windowWidth = jQuery(window).width();
    if (windowWidth < 900) {		
        jQuery('nav').addClass('navbar-fixed-top menu-topo ');
	    
    }			
	else{
		jQuery('nav').removeClass('navbar-fixed-top menu-topo');
		}
    jQuery('[data-spy="scroll"]').each(function () {
        var $spy = jQuery(this).scrollspy('refresh');
    });

});


