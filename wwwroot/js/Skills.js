document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.increment-btn').forEach(function(btn) {
        btn.addEventListener('click', function () {
            const skillId = btn.dataset.skillId;
            const slider = document.getElementById('ratingSlider-' + skillId);
            const valueSpan = document.getElementById('sliderValue-' + skillId);
            let current = parseFloat(slider.value);
            let step = parseFloat(slider.step); // Use slider's step attribute
            let max = parseFloat(slider.max);
            let next = Math.min(current + step, max);
            slider.value = next.toFixed(1); // Fix precision to 0.1
            valueSpan.textContent = slider.value + ' / ' + max;
        });
    });

    document.querySelectorAll('.decrement-btn').forEach(function(btn) {
        btn.addEventListener('click', function () {
            const skillId = btn.dataset.skillId;
            const slider = document.getElementById('ratingSlider-' + skillId);
            const valueSpan = document.getElementById('sliderValue-' + skillId);
            let current = parseFloat(slider.value);
            let step = parseFloat(slider.step); // Use slider's step attribute
            let min = parseFloat(slider.min);
            let next = Math.max(current - step, min);
            slider.value = next.toFixed(1); // Fix precision to 0.1
            valueSpan.textContent = slider.value + ' / ' + slider.max;
        });
    });

});
function changeTheme(theme) {
    const body = document.body;
    body.className = ''; // Reset existing classes
    reloadRadarChart(theme);

    const tables = document.querySelectorAll('table');
    const cards = document.querySelectorAll('.card');
    const inputs = document.querySelectorAll('input');

    switch (theme) {
        case 'orange':
            body.style.backgroundColor = '#E59400';
            body.style.color = '#000';
            tables.forEach(table => {
                table.style.backgroundColor = '#FFB347';
                table.style.color = '#000';
            });
            cards.forEach(card => {
                card.style.backgroundColor = '#FFA500';
                card.style.color = '#000';
            });
            inputs.forEach(input => {
                input.style.backgroundColor = '#FFB347';
                input.style.color = '#000';
            });
            break;
        case 'blue':
            body.style.backgroundColor = '#0056A3';
            body.style.color = '#FFF';
            tables.forEach(table => {
                table.style.backgroundColor = '#6CA6CD';
                table.style.color = '#000';
            });
            cards.forEach(card => {
                card.style.backgroundColor = '#007BFF';
                card.style.color = '#FFF';
            });
            inputs.forEach(input => {
                input.style.backgroundColor = '#6CA6CD';
                input.style.color = '#000';
            });
            break;
        case 'purple':
            body.style.backgroundColor = '#4B0082';
            body.style.color = '#FFF';
            tables.forEach(table => {
                table.style.backgroundColor = '#9370DB';
                table.style.color = '#000';
            });
            cards.forEach(card => {
                card.style.backgroundColor = '#6A0DAD';
                card.style.color = '#FFF';
            });
            inputs.forEach(input => {
                input.style.backgroundColor = '#9370DB';
                input.style.color = '#000';
            });
            break;
        case 'darkmode':
            body.style.backgroundColor = '#0D0D0D';
            body.style.color = '#FFF';
            tables.forEach(table => {
                table.style.backgroundColor = '#333333';
                table.style.color = '#FFF';
            });
            cards.forEach(card => {
                card.style.backgroundColor = '#121212';
                card.style.color = '#FFF';
            });
            inputs.forEach(input => {
                input.style.backgroundColor = '#333333';
                input.style.color = '#FFF';
            });
            break;
        case 'lightmode':
            body.style.backgroundColor = '#E6E6E6';
            body.style.color = '#000';
            tables.forEach(table => {
                table.style.backgroundColor = '#F5F5F5';
                table.style.color = '#000';
            });
            cards.forEach(card => {
                card.style.backgroundColor = '#F8F9FA';
                card.style.color = '#000';
            });
            inputs.forEach(input => {
                input.style.backgroundColor = '#F5F5F5';
                input.style.color = '#000';
            });
            break;
        case 'yellow':
            body.style.backgroundColor = '#E6E600';
            body.style.color = '#000';
            tables.forEach(table => {
                table.style.backgroundColor = '#FFFF99';
                table.style.color = '#000';
            });
            cards.forEach(card => {
                card.style.backgroundColor = '#FFFF00';
                card.style.color = '#000';
            });
            inputs.forEach(input => {
                input.style.backgroundColor = '#FFFF99';
                input.style.color = '#000';
            });
            break;
    }
}

const ctx = document.getElementById('categoryRadarChart').getContext('2d');
let radarChart = new Chart(ctx, {
    type: 'radar',
    data: {
        labels: JSON.parse(document.getElementById('categoryRadarChart').dataset.labels),
        datasets: [{
            label: 'Category Average',
            data: JSON.parse(document.getElementById('categoryRadarChart').dataset.data),
            fill: true,
            backgroundColor: 'rgba(0, 123, 255, 0.2)',
            borderColor: 'rgba(0, 123, 255, 1)',
            pointBackgroundColor: 'rgba(0, 123, 255, 1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(0, 123, 255, 1)'
        }]
    },
    options: {
        responsive: true,
        scales: {
            r: {
                min: 0,
                max: 10,
                ticks: {
                    stepSize: 1
                }
            }
        }
    }
});

function reloadRadarChart(theme) {
    console.log('Reloading radar chart with theme:', theme); // Debug log
    const radarChartCanvas = document.getElementById('categoryRadarChart');
    const ctx = radarChartCanvas.getContext('2d');

    let backgroundColor, borderColor, pointBackgroundColor, pointHoverBorderColor;

    switch (theme) {
        case 'orange':
            backgroundColor = 'rgba(255, 100, 0, 0.5)';
            borderColor = 'rgba(255, 100, 0, 1)';
            pointBackgroundColor = 'rgba(255, 100, 0, 1)';
            pointHoverBorderColor = 'rgba(255, 100, 0, 1)';
            break;
        case 'blue':
            backgroundColor = 'rgba(0, 80, 200, 0.5)';
            borderColor = 'rgba(0, 80, 200, 1)';
            pointBackgroundColor = 'rgba(0, 80, 200, 1)';
            pointHoverBorderColor = 'rgba(0, 80, 200, 1)';
            break;
        case 'purple':
            backgroundColor = 'rgba(75, 0, 130, 0.5)';
            borderColor = 'rgba(75, 0, 130, 1)';
            pointBackgroundColor = 'rgba(75, 0, 130, 1)';
            pointHoverBorderColor = 'rgba(75, 0, 130, 1)';
            break;
        case 'darkmode':
            backgroundColor = 'rgba(30, 30, 30, 0.5)';
            borderColor = 'rgba(30, 30, 30, 1)';
            pointBackgroundColor = 'rgba(30, 30, 30, 1)';
            pointHoverBorderColor = 'rgba(30, 30, 30, 1)';
            break;
        case 'lightmode':
            backgroundColor = 'rgba(200, 200, 200, 0.5)';
            borderColor = 'rgba(200, 200, 200, 1)';
            pointBackgroundColor = 'rgba(200, 200, 200, 1)';
            pointHoverBorderColor = 'rgba(200, 200, 200, 1)';
            break;
        case 'yellow':
            backgroundColor = 'rgba(200, 200, 0, 0.5)';
            borderColor = 'rgba(200, 200, 0, 1)';
            pointBackgroundColor = 'rgba(200, 200, 0, 1)';
            pointHoverBorderColor = 'rgba(200, 200, 0, 1)';
            break;
    }

    if (radarChart) {
        radarChart.destroy();
    }

    radarChart = new Chart(ctx, {
        type: 'radar',
        data: {
            labels: JSON.parse(radarChartCanvas.dataset.labels).map(label => label.toUpperCase()),
            datasets: [{
                label: 'Category Average',
                data: JSON.parse(radarChartCanvas.dataset.data),
                fill: true,
                backgroundColor: backgroundColor,
                borderColor: borderColor,
                pointBackgroundColor: pointBackgroundColor,
                pointBorderColor: '#fff',
                pointHoverBackgroundColor: '#fff',
                pointHoverBorderColor: pointHoverBorderColor
            }]
        },
        options: {
            responsive: true,
            scales: {
                r: {
                    min: 0,
                    max: 10,
                    ticks: {
                        stepSize: 1,
                        font: {
                            weight: 'bold',
                            size: 15 // Make ticks smaller
                        },
                        backdropColor: 'rgba(0, 0, 0, 0)' // Make label background transparent
                    },
                    pointLabels: {
                        font: {
                            size: 18,
                            weight: 'bold' // Make category labels bigger
                        }
                    }
                }
            },
            plugins: {
                legend: {
                    labels: {
                        font: {
                            size: 20,
                            weight: 'bold' // Make legend labels bold
                        }
                    }
                }
            }
        }
    });
    console.log('Radar chart reloaded successfully'); // Debug log
}
