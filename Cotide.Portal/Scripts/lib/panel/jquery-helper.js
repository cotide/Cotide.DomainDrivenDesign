// JavaScript Document
$.fn.extend({
	displaySearchPanel:function(){
		var id =$(this).attr("tag");
		$(this).click(function(){
			if($(this).find("i").attr("class")=="portlet-collapse-icon"){
				$(this).find("i").attr("class","");
				$(this).find("i").attr("class","portlet-expand-icon");
				$(this).find("span").text("点击显示搜索条件 ");
				$("#"+id).css("display","none")
				}
			else{
				$(this).find("i").attr("class","")
				$(this).find("i").attr("class","portlet-collapse-icon");
				$(this).find("span").text("点击隐藏搜索条件");
					$("#"+id).css("display","")
					}
			
			})
		
		},
	tablelist:function(options){ 
			var defaults = { 
			evenRowClass:"evenrow", 
			oddRowClass:"oddrow", 
			activeRowClass:"activerow" 
			} 
			var options = $.extend(defaults, options); 
			this.each(function(){ 
			var thisTable=$(this); 
			//添加奇偶行颜色 
			$(thisTable ).find("tbody").find("tr:even").addClass(options.evenRowClass); 
			$(thisTable).find("tbody").find("tr:odd").addClass(options.oddRowClass); 
			//添加活动行颜色 
			$(thisTable).find("tbody").find("tr").bind("mouseover",function(){ 
			$(this).addClass(options.activeRowClass); 
			}); 
			$(thisTable).find("tbody").find("tr").bind("mouseout",function(){ 
			$(this).removeClass(options.activeRowClass); 
			}); 
			}); 
			}		
		
	}); 
function initMenu() {
  $(".menu ul").hide();
  $(".menu ul:first").show();
  $(".menu>li>a").click(
    function() {
      var checkElement = $(this).next();
      if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
        return false;
        }
      if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
        $('.menu  ul:visible').slideUp('normal');
        checkElement.slideDown('normal');
        return false;
        }
      }
    );
  }