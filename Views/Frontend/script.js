document.getElementById('login-btn').addEventListener('click', function() {
    var slider = document.querySelector('.slider');
    slider.classList.remove('slider-move');  

    document.getElementById('login-form').style.display = 'block';
    document.getElementById('signup-form').style.display = 'none';
});

document.getElementById('signup-btn').addEventListener('click', function() {
    var slider = document.querySelector('.slider');
    slider.classList.add('slider-move');    

    document.getElementById('signup-form').style.display = 'block';
    document.getElementById('login-form').style.display = 'none';
});

/*

document.getElementById('form').addEventListener('submit', function(event) {
    event.preventDefault();

    const formData = new FormData(event.target);
    const data = {};
    formData.forEach((value, key) => {
        data[key] = value;
    });

    fetch('https://your-backend-endpoint/api', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })
    .then(response => response.json()) 
    .then(data => {
        console.log(data); 
    })
    .catch(error => {
        console.error('There was an error!', error);
    });
});

*/