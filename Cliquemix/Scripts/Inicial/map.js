$(function() {
		function load_map(keyword,category){
		var map = new google.maps.Map(document.getElementById('indexmap'), {
			zoom: 2,     
			mapTypeId: google.maps.MapTypeId.ROADMAP
		});
	
		var infowindow = new google.maps.InfoWindow();
	
		var marker, i;
		var markers = new Array();
		var jsondt;
		var jsonfile;				
		var results = [];
		
		$.getJSON("data/data.json", function(data) { 							
		  	jsondt = data.markersmap;		  
			for(i = 0; i < jsondt.length; i++) { 
			  if(keyword != 'all' && category != 'all'){
				  if(jsondt[i].country == keyword && jsondt[i].category == category){
					  results.push(jsondt[i]);
				  }
			  }else if(keyword == 'all' && category != 'all'){ 
				  if(jsondt[i].category == category){
					  results.push(jsondt[i]);
				  }
			  }else if(keyword != 'all' && category == 'all'){
				  if(jsondt[i].country == keyword){
					 results.push(jsondt[i]);
				  }
			  }else{
					results.push(jsondt[i]);				
			  }
			}
														
			for(i = 0; i < results.length; i++) {  
				marker = new google.maps.Marker({
				  position: new google.maps.LatLng(results[i].latitude, results[i].longitude),
				  map: map,
				  icon: results[i].image
				});
		
				markers.push(marker);
		
				google.maps.event.addListener(marker, 'click', (function(marker,i) { 
					return function() { 
						infowindow.setContent(results[i].content);
						infowindow.open(map, this);						
					}
			  	})(marker, i));
				
			}
			
			var bounds = new google.maps.LatLngBounds();
				$.each(markers, function (index, marker) {
				bounds.extend(marker.position);
			});      
			map.fitBounds(bounds);
			map.setZoom(2); 
		});	
	}
	
		var data = [{ "label": "Alabama"},{ "label": "Alaska"},{ "label": "Arizona"},{"label": "Arkansas"},{"label": "California"},{"label": "Colorado"},{"label": "Connecticut"},{"label": "Delaware"},{"label": "Florida"},{"label": "Georgia"},{"label": "Hawaii"},{"label": "Idaho"},{"label": "Illinois"},{"label": "Indiana"},{"label": "Iowa"},{"label": "Kansas"},{"label": "Kentucky"},{"label": "Louisiana"},{"label": "Maine"},{"label": "Maryland"},{"label": "Massachusetts"},{"label": "Michigan"},{"label": "Minnesota"},{"label": "Mississippi"},{"label": "Missouri"},{"label": "Montana"},{"label": "Nebraska"},{"label": "Nevada"},{"label": "New Hampshire"},{"label": "New Jersey"},{"label": "New Mexico"},{"label": "New York"},{"label": "North Carolina"},{"label": "North Dakota"},{"label": "Ohio"},{"label": "Oklahoma"},{"label": "Oregon"},{"label": "Pennsylvania"},{"label": "Rhode Island"},{"label": "South Carolina"},{"label": "South Dakota"},{"label": "Tennessee"},{"label": "Texas"},{"label": "Utah"},{"label": "Vermont"},{"label": "Virginia"},{"label": "Washington"},{"label": "West Virginia"},{"label": "Wisconsin"},{"label": "Wyoming"} ]
	
		$("#keyword").autocomplete({
			source: data,
			minLength: 1,	
			appendTo: "#search-group"		
		});	 
		
		$("#searchmapbutton").click(function(){
			var keyw = $("#keyword").val();
			var cate = $("#category").val();
			if(keyw!="" && cate!=""){
				load_map(keyw,cate);	
			}else if(keyw != "" && cate==""){
				load_map(keyw,'all');
			}else if(keyw == "" && cate!=""){
				load_map('all',cate);
			}else{
				load_map('all','all');
			}  			
		});
		
		load_map('all','all');
	});		