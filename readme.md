# Astro Surveyor

**Съдържание**

[Обобщение на реализирания проект](#обобщение-на-реализирания-проект)

[Описание на първоначалната концепция](#описание-на-първоначалната-концепция)

[Планиран обхват на проекта](#планиран-обхват-на-проекта)

[Вдъхновение](#вдъхновение)

[Действия на играча](#действия-на-играча)

[Принципна логика](#принципна-логика)


## Обобщение на реализирания проект
Достигнатия етап на разработка включва:
- основно меню на играта с възможност за избор на ниво
- базов потребителски интерфейс по време на игра, с индикатори за инвентар и възможност за взаимодействие при приближаване на подходящ обект/зона
- изяснена схема на управление на героя и неговото взаимодействие с околната среда;
- възможност за пренасяне на обекти и съхранението им в инвентар;
- възможност на героя да взаимодейства с обекти (инструменти, геологически формации), както и обектите да взаимодействат помежду си (научен уред да извлича проба от вид формация, за която е предназначен);
- възможност за приключване на нивото и определяне на постигнатия резултат чрез точкуване;
- базово Tutorial-ниво, което да научи играча на основните механики;
- завършено първо ниво;
- започнато второ ниво, което включва демонстрация на механики, които не са включени в другите нива.

### Управление на героя
Фокуса е върху използване на контролер (XBox, съвместим с PC) и указанията в интерфейса по време на игра са насочени към тази форма на управление. Създадено е и клавиатурно съответствие, достъпно паралелно (не е необходима настройка).
- Лява ръчка на контролер / стрелки на клавиатура / WASD на клавиатура - движение на героя по кардинални посоки
- Бутон A (юг) на контролер / Spacebar на клавиатура - взимане и оставяне на преносим обект
- Бутон X (запад) на контролер / Enter на клавиатура - интеракция с обекти / анализиране на формация
- Бутон Y (север) на контролер / Q на клавиатура - активиране на финал
- D-Pad на контролер / 1, 2, 3, 4 на клавиатура - прехвърляне на преносим обект от ръце в съответния слот на инвентара и обратно

### Цел на играта
Играча трябва да  излседва (визуално, чрез разхождане из нивото) игровата площ и да използва предоставените му уреди, за да събере максимално количество проби от околната среда и да ги транспортира обратно до короба, с който е пристигнал. Точкуването става на база общ брой проби и вида на пробите, като приоритета е да се извлекат повече различни видове проби, вместо голямо количество от един и същ вид - всеки нов вид проба носи +1000 точки, а индивидуалните проби са от 50 до 500 точки съответно за обикновените и за най-редките екземпляри.

### Тактически елементи
На този етап играта няма състояние на провал (failure state), но елементите за оптимизация са на лице - героя разполага със силно ограничен инвентар, който е споделен за инструментите и извлечените проби, съответно е необходима приоритизация на пробите с по-висока стойност. При въвеждане на времево ограничение за завършване на всяко ниво, играча трябва да планира добре експедицията си и/или да преиграва нивата до откриване на оптимална комбинация от действия, довеждаща до събиране на максимален брой точки за определеното време.

### Следващи стъпки
Изключвайки очевидното полиране на графичните елементи, интерфейса и добавянето на звукови ефекти, възможните пътища за продължение на проекта са описани в под-секциите Вторичен и Третичен игрови цикли на секция Действия на играча.

Като търсена пазарна реализация, тъй като продължителността на едно ниво може да се сведе до 3-5 минути и съществува елемент на оптимална игра, изискващ преиграване, продукта може да се оформи като мобилно приложение - кратката продължителност на една игрова сесия е добре приложима за запълване на "празното" време на потребителите, като напр. в градския транспорт, или чакайки на опашка и др. Схемата на управление е достатъчно проста, за да бъде реализирана само с един вид действие от страна на потребителя, напр. кратко натискане по екрана за придвижване на героя в избраната посока или взимане на посочен обект и задържане за взаимодейсвие с инструмент или формация. Предложените вторичен и третичен игрови цикли са добре съвместими както с козметични, така и с геймплей-афектиращи системи на монетизация.

## Описание на първоначалната концепция
Играча е астронавт, на изследователска мисия в чуждопланетна екосистема. Задачата му е да събере и анализира проби от явления на планетата (атмосфера, почва, растения, животни), като преминава между различни биосфери, с нарастваща сложност. За целта, разполага с различни научни инструменти, които трябва да се пренесат от кораба до мястото за вземане на пробата/извършване на експеримента.

## Планиран обхват на проекта
Проекта включва изготвяне на концепция за игра и реализация на основния и вторичен игрови цикъл, с продължителност поне едно цялостно ниво, за което е дефинирано успешно крайно състояние (възможност нивото да бъде завършено с „победа“, чрез изпълнение на поредица от действия от страна на играча). Към програмния продукт се включва и техническа документация.

## Вдъхновение
Идеята идва от три популярни игри: Black Mesa, Kerbal Space Program и Overcooked.

### Black Mesa
Средата е вдъхновена от главата Xen на играта Black Mesa, в частност множеството локации с видима научна дейност (геодезически маркери, скенери, разкрити почвени проби) и полу-разрушената научна станция, която играча посещава.
   
### KSP
В Kerbal Space Program играча може да избере да монтира научен уред на космически апарат и да изпълни експеримент чрез него, което носи различно количество точки, в зависимост от средата, с намаляваща ефективност ако е изпълнен в една и съща среда повече от веднъж.
  
### Overcooked
Основния метод на взаимодействие с околната среда ще бъде подобен на Overcooked, където игровия герой може пряко да взима и пренася обект, да обработва хранителни продукти чрез задържане на бутон и да ги комбинира чрез просто пренасяне  и поставяне един върху в подходящ съд.
   
## Действия на играча
### Основен игрови цикъл
- Управление в 2 измерения, в поглед отгоре, 3/4 изометрия
- Чрез натискане на бутон, игровия герой може да разглежда/анализира околната среда в непосредствена близост, което разкрива потенциални източници на проби
- Откриването на източници за проби може да бъде подобрено (като разстояние на засичане и видове валидни източници) чрез различни скенери – стационарни (които трябва да се монтират) или преносими
- Чрез натискане на бутон, игровия герой може да взима близкостоящ обект в ръце или да поставя държания обект
- Игровия герой може да носи само един обект в ръце
- Чрез натискане на бутон в близост до кораба на играча могат да бъдат “поръчвани” различни уреди за анализ и взимане на проби, под формата на преносими контейнери
- Уредите за взимане на проби се нуждаят от подходящи условия, за да работят, напр. електричество, време за приключване на работа, контролен панел, връзка с друг уред или действие от страна на играча
- Генерираният контейнер с проба трябва да се пренесе обратно в кораба на играча
### Вторичен игрови цикъл
Вторичния цикъл подлежи на балансиране по време на геймплей тестовете и част от описаните механики е възможно да не бъдат включени във финалната версия на проекта.
- Целта за успешно приключване на нивото може да включва:
  - Събиране на всички видове проби за съответното ниво
  - Постигане на определен брой точки от проби, без значение от техния вид и разнообразие
  - Откриване на конкретен вид проба
  - Събиране на максимално количество или разнообразие от проби или максимален брой точки
  - Изпълняване на поредица от мини-задания (напр. да се сканира конкретна зона, да се постави определен уред, да се постигне ниво на електрозахранване), като част от тях могат да бъдат задължителни, а други да носят само бонус
- Ограниченията/условията за провал на нивото могат да включват:
  - Фиксирано времетраене на нивото, напр. заради запасите кислород на игровия герой
  - Бюджет за “закупуване” на уредите за анализ
  - Фиксиран набор от инструменти за анализ в конкретното ниво, които трябва да се комбинират по максимално ефективен начин
### Третичен игрови цикъл (концептуален) 
Този цикъл включва дълготрайната прогресия на играча и не е част от текущия обхват на проекта. Предложените механики могат да бъдат реализирани при бъдещ интерес в проекта.
- Събраните проби се транспортират до основна лаборатория на повърхността на планетата, където се анализират
- Всяка проба носи различно количество научни точки
- Точките се използват за отпускане на допълнителен бюджет на мисията, който да се използва за отключване на нови възможности:
  - построяване на нови модули към лабораторията
  - инсталиране на допълнителни уреди за анализ (позволяващи извличане на повече точки от пробите или анализ на нови видове проби)
  - поръчване на допълнително оборудване за използване в мисия (напр. специален скафандър с термична защита, който да отключи достъпа до вулканични нива)
  - декорация (мебели в стаята за почивка, нова кафемашина за астронавтите и други козметични подобрения)
## Принципна логика
- Управлението на героя използва обновената Input-система на Unity, с възможност за използване на клавиатура и контролер, като фокуса ще бъде върху контролер
- Действията „Пренасяне“ и „Взаимодействие“ се контролират от компоненти върху героя, които търсят най-близкия валиден обект с реципрочен компонент, съответно „Преносим обект“ или „Интерактивен обект“
- „Преносим обект“ ще бъде наличен на повечето уреди, взети проби, преносими скенери
- В случай че Преносим обект“ и „Интерактивен обект“ са едновременно налични, обекта може да бъде пренасян само ако в момента не е активен, или при опит за пренасяне се деактивира; аналогично, обект не може да бъде активиран по време на пренос
- Компонента „Интерактивен обект“ може да съдържа списък от компоненти „Ефект“, които да се активират при интеракция
- Компонентите, реализиращи интерфейса „Ефект“ могат да съдържат списък от компоненти „Ресурс“ и списък от компоненти „Резултат“ – успешната активация зависи от наличието на всички ресурси и след завършване (или постоянно, докато са налични условията) произвежда съответния резултат/резултати
- Във ефекта е описано отношение консумиран ресурс за производство на единица резултат; ако производството е еднократно, консумацията е абсолютна стойност; ако е постоянно, консумацията е за изтекла единица време, напр. за 1 секунда
- „Ефектите“ включват сканиране на околната среда, взимане на проба, анализ/преработка на проба, произвеждане на ресурс, отваряне на потребителски интерфейс с менюта, превръщане в друг вид обект и др.
- „Ресурси“ и „Резултати“ включва време, сурови екземпляри (почва, минерали, растения и др.), проби, електричество, процесорни цикли, охлаждане, топлина, хидравлично налягане и др.
- Различните уреди се свързват по между си при близост или чрез конектори, които играча свързва ръчно