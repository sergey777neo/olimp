// Write your Javascript code.
const $arrow = document.querySelector('.arrow');
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