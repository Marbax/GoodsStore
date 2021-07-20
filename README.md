# GoodsStore

ASP .Net Framework/Core App

<dev ><img src="https://raw.githubusercontent.com/Marbax/CVAssistant/master/gitAnim/rainbow.gif" width="100%" height="25" margin = "200" align="center">
</dev>

<img src="https://octodex.github.com/images/daftpunktocat-thomas.gif" width="120" align="right">

<details style="padding: 1rem;"><summary><b>Требования</b></summary>

- Документация 
  - Титул : ФИО , название,
  - Куски кода
- UML диаграмыы
  - Диаграмма классов(бизнесс логики\ключевые)
  - Диаграмма use-case
  - Другие диаграммы по желанию
- Схема базы данных
- Презентация 10-15 слайдов о проекте 
  - Стек технологий
  - Цель проекта 
  - Почему такой проект 
  - Структура проекта
  - Какая архитектура (в виде схем) 
  - Схема бд
  - Скрины рабочего состояния проекта
- Полностью рабочий вариант приложения
- Мобильная версия обязательно
- Все кнопки рабочие ,никаких затычек
- Защита - аналог продажи продукта
- Веб приложение должно быть опубликовано
- Комиссия будет задавать вопросы
- 2 этапа
  - Пред защита (предварительная оценка)
  - Защита
- Время выступления 5-7 минут + вопросы комиссии  
- Каникулы три недели(консультаций не будет)
- Консультация - каждую неделю (демонстрация текущего результата дипломной работы)
- Защита диплома в сентябре
- Защита диплома в присутсвии(не онлайн)
</details>

<details style="padding: 1rem;"><summary><b>Projects</b></summary>

<details style="margin-left:2rem;"><summary>Domain</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Framework Lib</p>
        <p>Provide work with db , entities and context</p>
        <ul style="margin-top:1rem;padding-left:1rem;"><b>Dependecies</b>
            <li>Entity Framework 6.*</li>
            <li>LinqKit</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>Business.Models</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Standard Lib</p>
        <p>Business models , friendly for api with anotations</p>
        <ul style="margin-top:1rem;padding-left:1rem;"><b>Dependecies</b>
            <li>System.ComponentModel.DataAnnotations</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>JWTAuth</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Standard Lib</p>
        <p>Static class for JWT Bearer token based authentitication </p>
        <ul style="margin-top:1rem;padding-left:1rem;"><b>Dependecies</b>
            <li>System.IdentityModel.Tokens.Jwt</li>
            <li>Microsoft.IdentityModel.Tokens</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>Business</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Framework Lib</p>
        <p>Contains services and convert models from data layer to business</p>
        <ul style="margin-top:1rem;padding-left:1rem;"><b>Dependecies</b>
            <li>Domain</li>
            <li>JWT</li>
            <li>Business.Models</li>
            <li>Entity Framework 6.*</li>
            <li>AutoMapper</li>
            <li>AutoMapper.Extensions.ExpressionMapping</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>Infrastructure</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Framework Lib  </p>
        <p>Immutable DI</p>
        <ul style="margin-top:1rem;padding-left:1rem;"><b>Dependecies</b>
            <li>Entity Framework 6.*</li>
            <li>Ninject</li>
            <li>AutoMapper</li>
            <li>AutoMapper.Extensions.ExpressionMapping</li>
            <li>Ninject.Web.Common</li>
            <li>Ninject.Web.Common.WebHost</li>
            <li>Ninject.Web.WebApi</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>WebServer</summary>
    <div style="margin:0 0 2rem 1rem;">
        <ul style="margin-top:1rem;padding-left:1rem;"><b>Dependecies</b>
            <li>Infrastructure</li>
            <li>Business</li>
            <li>Business.Models</li>
            <li>Microsoft.AspNet.WebApi.Cors</li>
            <li>System.IdentityModel.Tokens.Jwt</li>
            <li>Microsoft.IdentityModel.Tokens</li>
 </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>Client</summary>
    <div style="margin:0 0 2rem 1rem;">
    <p>WebAssembly Progressive App</p>
    <p>with ViewModels</p>
    <p>and client services</p>
        <ul style="margin-top:1rem;padding-left:1rem;"><b>Dependecies</b>
            <li>Business.Models</li>
        </ul>
    </div>
</details>

</details>
