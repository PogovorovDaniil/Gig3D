# Библиотека для создания 3d графики на C#
Успешная попытка написать библиотеку 3d графики с использование winapi и amp
## Структура проекта
Решение состоит из 3-х проектов
1. Gig3D - реализация основных функций на C++, таких как:
    * Создание окна виндовс, взаимодействие с ним, и обработка событий окна
    * Вычисление 2d и 3d графики на графическом процессоре
2. Gig3Dbase - импорт функций из Gig3D на C#, создание оболочки удобной для пользования (объекты (классы) окна, спрайтов, 3d моделей, и т.д.)
3. Gig3DTest - пример использования выше описанных библиотек
## Запуск
Из за специфик работы проекта, после изменения и компиляции всего решения в release, рекомендуется запускать Gig3DTest с помошью скрипта /Gig3DTest/bin/Release/start.bat, а не запускать в debug.