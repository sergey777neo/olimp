initProgress();

$(document).ready(function () {
	loadUserLevel();
})

function loadUserLevel(id) {
	$("#game").load("/Game/LoadCurrentLevel" + (id ? "/" + id : ""), function () {
		if (id) {
			$(".game-page__current-level").removeClass("game-page__current-level");
			var newItem = $("[data-number='0"+id+"']");
			newItem.addClass("game-page__current-level");
			newItem[0].innerHTML = "";
			this[0].initProgress();
		}
	}.bind([this, id]));
}

function initProgress() {
	var current = $(".game-page__current-level");
	var bar = new ProgressBar.Circle(current[0], {
		strokeWidth: 6,
		easing: 'easeInOut',
		duration: 1400,
		text: {
			value: current.data("number")
		},
		color: 'white',
		trailColor: '#ffffff33',
		trailWidth: 1,
		svgStyle: null
	});
	bar.animate(current.data("current") / current.data("count"));
}