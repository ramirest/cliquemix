/**
 *	Master Slider, Partial View v1.0
 * 	@author: Averta Ltd.
 */

;(function(){
	
	window.MSPartialView = function(options){
		MSWaveView.call(this , options);
		this.$element.removeClass('ms-wave-view').addClass('ms-stf-view');
	};
	
	MSPartialView.extend(MSWaveView);
	MSPartialView._3dreq = true;
	
	var p = MSPartialView.prototype;
	var _super = MSWaveView.prototype;
	
	/*-------------- METHODS --------------*/
	
	p.__updateSlidesHoriz = function(slide , distance){
		var value =  Math.abs(distance * 100 / this.__width);
		slide.$bg_img.css('opacity' , (100 -  Math.abs(distance * 120 / this.__width / 3)) / 100);
		slide.$element.css(window._csspfx + 'transform' , 'translateZ('+ -value * 3 +'px) rotateY(0.01deg) translateX('+distance*0.75+'px)');
	};
	
	p.__updateSlidesVertic = function(slide , distance){
		this.__updateSlidesHoriz(slide , distance);
	};
	
	MSSlideController.registerView('prtialwave' , MSPartialView);
	
})();

;(function(){
	
	window.MSPartialView2 = function(options){
		MSWaveView.call(this , options);
		this.$element.removeClass('ms-wave-view').addClass('ms-stf-view');
	};
	
	MSPartialView2.extend(MSWaveView);
//	MSPartialView2._3dreq = true;

	var p = MSPartialView2.prototype;
	var _super = MSWaveView.prototype;
	
	/*-------------- METHODS --------------*/
	
	p.__updateSlidesHoriz = function(slide , distance){
		var value =  Math.abs(distance * 100 / this.__width);
		 value = Math.min(value , 100);
		slide.$element.css('opacity' , 1-value/300);
		slide.$element.css(window._csspfx + 'transform' , 'scale('+ (1 - value/800) +') rotateY(0.01deg) ');
	};
	
	p.__updateSlidesVertic = function(slide , distance){
		this.__updateSlidesHoriz(slide , distance);
	};
	
	MSSlideController.registerView('prtialwave2' , MSPartialView2);
	
})();

;(function(){
	
	window.MSPartialView3 = function(options){
		MSWaveView.call(this , options);
		this.$element.removeClass('ms-wave-view').addClass('ms-stf-view');
	};
	
	MSPartialView3.extend(MSWaveView);
	MSPartialView3._3dreq = true;

	var p = MSPartialView3.prototype;
	var _super = MSWaveView.prototype;
	
	/*-------------- METHODS --------------*/
	
	p.__updateSlidesHoriz = function(slide , distance){
		var value =  Math.abs(distance * 100 / this.__width);
		 value = Math.min(value , 100);
		 var rvalue =  Math.min(value * 50 / 100 , 50) * (distance < 0 ? -1 : 1);
		var zvalue = value * 20 / 100;
		slide.$element.css('opacity' , 1-value/300);
		slide.$element.css(window._csspfx + 'transform' , 'translateZ('+ -zvalue*5 +'px) rotateY(' + rvalue + 'deg) ');
	};
	
	p.__updateSlidesVertic = function(slide , distance){
		this.__updateSlidesHoriz(slide , distance);
	};
	
	MSSlideController.registerView('prtialwave3' , MSPartialView3);
	
})();