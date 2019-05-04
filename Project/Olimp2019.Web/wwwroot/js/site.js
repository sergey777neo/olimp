// Write your Javascript code.
const $arrow = document.querySelector('.index-page__after-header__button__arrow');
if ($arrow) {
	$arrow.animate([
		{ left: '0' },
		{ left: '10px' },
		{ left: '0' }
	], {
		duration: 700,
		iterations: Infinity
	});
}

//Trigger focusout on fields, that was autofill by browser to change placeholder position
$(document).ready(function () {
	setTimeout(function () {
		$(".login-page input.form-control[value!='']").focusout();
	})
})
