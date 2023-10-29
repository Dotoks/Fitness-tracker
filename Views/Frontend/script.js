document.getElementById('login-btn').addEventListener('click', function() {
    var slider = document.querySelector('.slider');
    slider.classList.remove('slider-move');  // Remove the class to move the slider back

    // Show login form and hide signup form
    document.getElementById('login-form').style.display = 'block';
    document.getElementById('signup-form').style.display = 'none';
});

document.getElementById('signup-btn').addEventListener('click', function() {
    var slider = document.querySelector('.slider');
    slider.classList.add('slider-move');     // Add the class to move the slider

    // Show signup form and hide login form
    document.getElementById('signup-form').style.display = 'block';
    document.getElementById('login-form').style.display = 'none';
});
