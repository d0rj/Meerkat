# Meerkat

⚠️ Work in progress! ⚠️

## Описание

Приложение для генерации текста из шаблонов (шаблонизатор), умеющее изменять форму слова. Частный случай применения - генерация аргументов против X, за Y и т.д.; для чего, собственно, и делалось.

## Использование

Слева - рабочая область, в которой сверху находится окно редактирования, а снизу расположено окно редактирвания переменных для шаблона.

Справа окно вывода.

Вообще, UI довольно простой, запутаться сложно)

## Шаблоны

### Создание шаблонов

Общий формат таков: **[ X | form modifier]**. Программа заменит такую запись на подходящую, основываясь на **form** и **modifier**.

#### **Переменные**

Название переменной должно соответствовать правилам:

*  Переменные состоят из прописных букв латинского алфавита, символа подчёркивания '**_**' и тире '**-**'.

Допускается передача в аргументах "лишних" переменных.

#### **Обработка слова**, **form**

На месте **form** может быть одна из строк, представляющих формы слов, соответствующая таблице (символы в фигурных скобках *{}* являются опциональными):

Используемая строка | Форма слова | Пояснение | Пример
------------------- | ----------- | --------- | ------
*пустая строка* |  | то же, что и **именительный** падеж |
**about** |  | **предложный** падеж, но с подходящим предлогом *о/об* | об интернете / о матче
**обо** |  | то же, что и **about** | 
**имен{ительный}** | именительный падеж | Кто? Что? | хомяк ест
**родит{ельный}** | родительный падеж | Кого? Чего? | у нас нету хомяка
**дате{льный}** | дательный падеж | Кому? Чему? | сказать хомяку спасибо
**вини{тельный}** | винительный падеж | Кого? Что? | хомяк читает книгу
**твор{ительный}** | творительный падеж | Кем? Чем? | зерно съедено хомяком
**предл{ожный}** | предложный падеж | О ком? О чём? и т.п. | хомяка несут в корзинке

#### **Модификаторы** (**modifier**)

Для существительных доступен модификатор **+**, указывающий на множественное число. Например:

[X+], [X | +] или [X| именительный+] при **X**=*яблоко* будет означать *яблоки*.

### Пример

При **X** = **кот** и **Y** = **молоко**:

* Выражение **[X|именительный]овасия**, **[X|имен]овасия**, **[X|]** или **[X]овасия** будет заменено на **котовасия**;

* Выражение **[X|form]** может иметь между словами сколько угодно пробелов, к примеру **[  X |form      ]**. Разницы после обработки не будет;

* **[Y] для [X | имен]** будет заменено на **молоко для кота**.

## Warning

**Works only with russian language**.
