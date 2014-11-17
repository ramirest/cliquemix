// JavaScript Document

$(document).ready(function(){	
	$('.mainmenumobile').click(function()
	{	$(this).toggleClass('minus')
		$(this).next('ul.mainmenucontain').slideToggle()
		$('ul.mainmenucontain li a.submenu2').click(function(e){
			e.isImmediatePropagationStopped()
			$(this).toggleClass('minus')				
			$(this).next('div.submenu').slideToggle()	
		})
	});

// Main Category
	$('.maincategory').carouFredSel({					
		prev: '#prevcat',
		next: '#nextcat',
		width: '100%',
		scroll: 1,
		items: {
		//	width: 200,
		//	height: '30%',	//	optionally resize item-height
			visible: {
				min: 2,
				max: 8
			}
		}
	});
	
// Prmium Listing
	$('#premiumlisting').carouFredSel({		
		responsive: true,
		auto: false,
		width: '100%',
		scroll: 1,
		items: {
			width: 200,
		//	height: '30%',	//	optionally resize item-height
			visible: {
				min: 2,
				max: 5
			}
		},
		prev: '#prevpre',
		next: '#nextpre',		
	});
				
// Main Menu mobile
	 $("<select />").appendTo(".menurelative");	  
	 // Create default option "Go to..."
	  $("<option />", {
		 "selected": "selected",
		 "value"   : "",
		 "text"    : "Go to..."
	  }).appendTo("nav.subnav select");      
	// Populate dropdown with menu items
      $("nav.subnav a[href]").each(function() {
       var el = $(this);
       $("<option />", {
           "value"   : el.attr("href"),
           "text"    : el.text()
       }).appendTo("nav.subnav select");
      });      
	 // To make dropdown actually work
      $("nav.subnav select").change(function() {
        window.location = $(this).find("option:selected").val();
      });

// Detail page Thumb Zoom
	$('.my-foto-container').imagezoomsl({ 
	 zoomrange: [1, 12],
         zoomstart: 4,
         innerzoom: true,
         magnifierborder: "none",	    
          magnifiersize: [500, 300],
          scrollspeedanimate: 10,
          loopspeedanimate: 5,          
         // magnifiereffectanimate: "slideIn"	
	}); 
	$ (".zoom" ).click( function () {
    var That =  this ;
    $( ".my-foto-container" ).fadeOut ( 100 , function (){
	 $(this).attr( "src" ,$ ( That).attr ( "src" ))            
     . attr ("data-large", $ (That).attr ("data-large")).fadeIn (200 )
	 . attr ("data-title", $ (That).attr ("data-title"))
	 . attr ("data-help", $ (That).attr ("data-help"))
				
           }); 
       }); 

// List & Grid View
	$('#serchlist .searchresult:first').show();
	$('#bloglist .bloglisting:first').show();
	$('#list').click(function(){	
	$(this).addClass ('btn-orange').children('i').addClass('icon-white')
		$('.grid').fadeOut()
		$('.list').fadeIn()
		$('#grid').removeClass ('btn-orange').children('i').removeClass('icon-white')
	});
	$('#grid').click(function(){
		$(this).addClass ('btn-orange').children('i').addClass('icon-white')
		$('.list').fadeOut()
		$('.grid').fadeIn()
		$('#list').removeClass ('btn-orange').children('i').removeClass('icon-white')
	});
	
//  Sparkline	
	$(".inlinesparkline").sparkline('html', {
		type: 'line',
		width: '100%',
		height: '200px',
		lineColor: '#999999',
		fillColor: '#E9E9E9',
		lineWidth: 3,
		spotColor: '#F3601D',
		minSpotColor: '#F3601D',
		maxSpotColor: '#F3601D',
		highlightSpotColor: '#F3601D',
		highlightLineColor: '#f4c3c4',
		spotRadius: 5,
		drawNormalOnTop: true,
	}); 

	$(".monthly-sales").sparkline([3,5,6,7,10,12,16,11,9,8.9,8.7], {
		type: 'bar',
		barColor: '#999999',
		height: '200px',
		width: '100%',
		barWidth:22,
		barSpacing: 8
	}); 
	
//  Accrodian	
	$("#accrodian").collapse({toggle: false})

//  Accrodian	
	var $acdata = $('.accrodian-data'),
		$acclick = $('.accrodian-trigger');

	$acdata.hide();
	$acclick.first().addClass('active').next().show();	
	
	$acclick.on('click', function(e) {
		if( $(this).next().is(':hidden') ) {
			$acclick.removeClass('active').next().slideUp(300);
			$(this).toggleClass('active').next().slideDown(300);
		}
		e.preventDefault();
	});
	
//  Accrodian	
	var $acdata1 = $('.accrodian-data-faq'),
		$acclick1 = $('.accrodian-trigger-faq');

	$acdata1.hide();
	$acclick1.first().addClass('active').next().show();	
	
	$acclick1.on('click', function(e) {
		if( $(this).next().is(':hidden') ) {
			$acclick1.removeClass('active').next().slideUp(300);
			$(this).toggleClass('active').next().slideDown(300);
		}
		e.preventDefault();
	});

// alert 
	$( '.alert .fa-times-circle').click(function()
	{
		$(this).parent('.alert').fadeOut(300)
	})
	
// Toggle			
	$('.togglehandle').click(function()
	{
		$(this).toggleClass('active')
		$(this).next('.toggledata').slideToggle()
	});
	
//Tab 
	$('#myTab a').click(function (e) {
		e.preventDefault()
  		$(this).tab('show');
	})
	$('#myTab a:first').tab('show') // Select first tab
	//Tab Why Us
	$('.myTabclass a').click(function (e) {
		e.preventDefault()
  		$(this).tab('show');
	})
	$('.myTabclass a:first').tab('show') // Select first tab

// Tooltip	
	$('.tooltip-test').tooltip();

// Scroll top
	$(window).scroll(function () {
		if ($(this).scrollTop() > 50) {
			$('#gotop').fadeIn(500);							
		} else {
			$('#gotop').fadeOut(500);
		}
		});	
	$('#gotop').click(function()
		{				
			$("html, body").animate({ scrollTop: 0 }, 600);
		})

// Coming Soon
	//cowntdown function. Set the date by modifying the date in next line (January 01, 2013 00:00:00):
		var austDay = new Date("December 31, 2013 00:00:00");
		
			$('#comingsoon').countdown({until: austDay, layout: '<div class="box"><div>{dn}</div> <span> {dl} </span></div> <div class="box"><div>{hn}</div> <span> {hl} </span></div> <div class="box"><div>{mn}</div> <span> {ml} </span></div> <div class="box"><div>{sn}</div> <span> {sl} </span></div>'});
			$('#year').text(austDay.getFullYear());

// google map	
$("#contactmap").gMap({
	address: "pxcreate, Elizabeth Street, Melbourne, Victoria, Australia",//replace this with your address
	zoom: 10,
	markers:[
		{
			latitude: -37.817361, //replace this with your latitude
			longitude: 144.965047,//replace this with your longitude
			html: "pxcreate Pty Ltd" //replace this with your text
		}		
	]
});
	
// Social Steams 
   $('.flickr-feed').socialstream({
		socialnetwork: 'flickr',
		limit: 16,
		username: 'flickr', 
	})	
	$('.youtube-feed').socialstream({
		socialnetwork: 'youtube',
		limit: 16,
		username: 'youtube'
	})



// Twitter
	$("#twitter").tweet({
	join_text: "auto",
	username: "twitter", //replace this with your username
	modpath: './twitter/',
	avatar_size: 32,
	count: 2,
	auto_join_text_default: ",",
	auto_join_text_ed: "",
	auto_join_text_ing: "",
	auto_join_text_reply: "",
	auto_join_text_url: "",
	loading_text: "loading tweets..."
	});

// Contact Form 
	$(".contactform").validate({
   submitHandler: function(form) {
	   var name = $("input#name").val();
	   var email = $("input#email").val();
	   var url = $("input#url").val();
	   var message = $("textarea#message").val();
	   
	   var dataString = 'name='+ name + '&email=' + email + '&url=' + url+'&message='+message;
	  $.ajax({
	  type: "POST",
	  url: "email.php",
	  data: dataString,
	  success: function() {
		  $('#contactmsg').remove();
		  $('.contactform').prepend("<div id='contactmsg' class='successmsg'>Form submitted successfully!</div>");
		   $('#contactmsg').delay(1500).fadeOut(500);
		  $('#submit_id').attr('disabled','disabled');
		  }
		});   
   return false;
	}
	});
	
	$('#blogslider').flexslider({
				animation: "slide",		
				start: function(slider){
					$('body').removeClass('loading');
				},				
			});	

// Cloud Plugin

var word_list = [
        {text: "Lorem", weight: 13, link: "https://github.com/DukeLeNoir/jQCloud"},
        {text: "Ipsum", weight: 10.5, html: {title: "My Title", "class": "custom-class"}, link: {href: "http://jquery.com/", target: "_blank"}},
        {text: "Dolor", weight: 9.4},
        {text: "Sit", weight: 8},
        {text: "Amet", weight: 6.2},
        {text: "Consectetur", weight: 5},
        {text: "Adipiscing", weight: 5},
        {text: "Elit", weight: 5},
        {text: "Nam et", weight: 5},
        {text: "Leo", weight: 4},
        {text: "Sapien", weight: 4},
        {text: "Pellentesque", weight: 3},
        {text: "habitant", weight: 3},
        {text: "morbi", weight: 3},
        {text: "tristisque", weight: 3},
        {text: "senectus", weight: 3},
        {text: "et netus", weight: 3},
        {text: "et malesuada", weight: 3},
        {text: "fames", weight: 2},
        {text: "ac turpis", weight: 2},
        {text: "egestas", weight: 2},
        {text: "Aenean", weight: 2},
        {text: "vestibulum", weight: 2},
        {text: "elit", weight: 2},
        {text: "sit amet", weight: 2},
        {text: "metus", weight: 2},
        {text: "adipiscing", weight: 2},
        {text: "ut ultrices", weight: 2},
        {text: "justo", weight: 1},
        {text: "dictum", weight: 1},
        {text: "Ut et leo", weight: 1},
        {text: "metus", weight: 1},
        {text: "at molestie", weight: 1},
        {text: "purus", weight: 1},
        {text: "Curabitur", weight: 1},
        {text: "diam", weight: 1},
        {text: "dui", weight: 1},
        {text: "ullamcorper", weight: 1},
        {text: "id vuluptate ut", weight: 1},
        {text: "mattis", weight: 1},
        {text: "et nulla", weight: 1},
        {text: "Sed", weight: 1}
      ];
      $(function() {
        $("#cloud").jQCloud(word_list);
      });
	  
	  
	
});