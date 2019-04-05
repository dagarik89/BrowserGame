var rand = function (min, max) { k = Math.floor(Math.random() * (max - min) + min); return Math.round(k / s) * s; };
//создание еды
var newA = function () { a = [rand(0, innerWidth), rand(0, innerHeight)]; },
    //тело змейки
    newB = function () { sBody = [{ x: 0, y: 0 }]; };
var snake = document.getElementById('snake'),
    ctx = snake.getContext('2d'),
    sBody = null, //Начально тело змейки - два элемента
    direction = 1, //Направление змейки: 1 - направо, 2 - вниз 3 - влево, 4 - вверх
    a = null, //Еда: массив, 0 элемент - x, 1 элемент - y
    s = parseInt(document.getElementById('size').value); newB(); newA(); //Создаем змейку
//document.getElementById('max_result').value = 5;
var m_res = parseInt(document.getElementById('max_result').value),
    res = parseInt(document.getElementById('result').value);

setInterval(function () {
    if (a[0] + s >= snake.width || a[1] + s >= snake.height) newA(); //Если еда вне границ
    ctx.clearRect(0, 0, snake.width, snake.height); //Очищаем старое
    ctx.fillStyle = document.getElementById('food_color').value;
    ctx.fillRect(...a, s, s); //Рисуем еду
    ctx.fillStyle = document.getElementById('snake_color').value;
    sBody.forEach(function (el, i) {
        if (el.x == sBody[sBody.length - 1].x && el.y == sBody[sBody.length - 1].y && i < sBody.length - 1) {
            if (res > m_res) { m_res = res, document.getElementById('max_result').value = m_res;} document.getElementById('result').value = 0, res = 0, sBody.splice(0, sBody.length - 1), sBody = [{ x: 0, y: 0 }], direction = 1; } //Проверка на столкновение
    });
    var m = sBody[0], f = { x: m.x, y: m.y }, l = sBody[sBody.length - 1]; // сохраняем хвост и голову змейки
    if (direction == 1) f.x = l.x + s, f.y = Math.round(l.y / s) * s; //Если направление вправо, то тогда сохраняем Y, но меняем X на + s
    if (direction == 2) f.y = l.y + s, f.x = Math.round(l.x / s) * s; // Если направление вниз, то сохраняем X, но меняем Y на + s
    if (direction == 3) f.x = l.x - s, f.y = Math.round(l.y / s) * s; //Если направление влево, то сохраняем Y, но меняем X на -s
    if (direction == 4) f.y = l.y - s, f.x = Math.round(l.x / s) * s; //Если направление вверх, то сохраняем X, Но меняем Y на -s
    sBody.push(f); //Добавляем хвост после головы с новыми координатами
    sBody.splice(0, 1); //Удаляем хвост
    //Отрисовываем каждый элемент змейки
    sBody.forEach(function (pob, i) {
        if (direction == 1) if (pob.x > Math.round(snake.width / s) * s) pob.x = 0; //Если мы двигаемся вправо, то если позиция эемента по X больше, чем ширина экрана, то ее надо обнулить
        if (direction == 2) if (pob.y > Math.round(snake.height / s) * s) pob.y = 0; //Если мы двигаемся внизу, то если позиция элемента по X больше, чем высота экрана, то ее надо обнулить
        if (direction == 3) if (pob.x < 0) pob.x = Math.round(snake.width / s) * s; //Если мы двигаемся влево, и позиция по X меньше нуля, то мы ставим элемент в самый конец экрана (его ширина)
        if (direction == 4) if (pob.y < 0) pob.y = Math.round(snake.height / s) * s; //Если мы двигаемся вверх, и позиция по Y меньше нуля, то мы ставим элемент в самый низ экрана (его высоту)
        if (pob.x == a[0] && pob.y == a[1]) res = res + 1, document.getElementById('result').value = res, newA(), sBody.unshift({ x: f.x - s, y: l.y }); //Проверка на то, что змейка съела еду
        ctx.fillRect(pob.x, pob.y, s, s);
        // s - это ширина и высота нашего "квадрата"
    });
}, 1000 / parseInt(document.getElementById('speed').value)); //Скорость
onkeydown = function (e) {
    var key = e.keyCode;
    if ([38, 39, 40, 37].indexOf(key) >= 0)
        //Останавливаем событие, отменяем его действие по умолчанию. Например, при нажатии на стрелочку вверх мог произойти скролл, но он не произойдет, так как мы его отменили
        e.preventDefault();
    if (key == 39 && direction != 3) direction = 1; //Вправо
    if (key == 40 && direction != 4) direction = 2; //Вниз
    if (key == 37 && direction != 1) direction = 3; //Влево
    if (key == 38 && direction != 2) direction = 4; //Вверх
};