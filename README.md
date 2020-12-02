# GoodsStore

ASP .Net Framework/Core App

<details><summary>Projects</summary>

<details style="margin-left:2rem;"><summary>Domain</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Framework Lib</p>
        <p>Works with db</p>
        <ul style="margin-top:1rem;padding-left:1rem;">Dependecies
            <li>Entity Framework 6.*</li>
            <li>LinqKit</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>Business.Models</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Standard Lib</p>
        <p>Business models , friendly for api</p>
        <ul style="margin-top:1rem;padding-left:1rem;">Dependecies
            <li>System.ComponentModel.DataAnnotations</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>JWTAuth</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Standard Lib</p>
        <p>Static class for JWT Bearer token based authentitication </p>
        <ul style="margin-top:1rem;padding-left:1rem;">Dependecies
            <li>System.IdentityModel.Tokens.Jwt</li>
            <li>Microsoft.IdentityModel.Tokens</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>Business</summary>
    <div style="margin:0 0 2rem 1rem;">
        <p>.Net Framework Lib</p>
        <p>Contains services and convert models from data layer to business</p>
        <ul style="margin-top:1rem;padding-left:1rem;">Dependecies
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
        <ul style="margin-top:1rem;padding-left:1rem;">Dependecies
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
        <ul style="margin-top:1rem;padding-left:1rem;">Dependecies
            <li>Infrastructure</li>
            <li>Business</li>
            <li>Business.Models</li>
            <li>Microsoft.AspNet.WebApi.Cors</li>
        </ul>
    </div>
</details>

<details style="margin-left:2rem;"><summary>Client</summary>
    <div style="margin:0 0 2rem 1rem;">
    WebAssembly Progressive App  
    with ViewModels  
    and client services
        <ul style="margin-top:1rem;padding-left:1rem;">Dependecies
            <li>Business.Models</li>
        </ul>
    </div>
</details>

</details>
