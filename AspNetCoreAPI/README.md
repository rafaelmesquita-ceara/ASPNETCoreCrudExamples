# Sobre o AspNetCoreAPI

O AspNetCoreAPI é uma API simples feita em **ASP.NET CORE 3.1** utilizando o **Entity Framework** como ORM, o projeto foi feito para fins de aprendizado. Conceitos como métodos http, migrations, CLI do dotnet, validações com base no modelo, padrão de CodeFirst, e SQL Server hospedado no AZURE foram aplicados de forma prática nesta solução.

<img src="https://user-images.githubusercontent.com/62113721/87359739-cf9c6980-c53e-11ea-8841-4d0c7ae868c3.png" 
width="95%" height="400px"
/>

<img src="https://user-images.githubusercontent.com/62113721/87359749-d3c88700-c53e-11ea-967f-2fdeba54a282.png" 
width="95%" height="400px"
/> 

 
 

# Entidades
O AspNetCoreAPI consiste em uma entidade de categorias e uma entidade de produtos onde possui a seguinte estrutura relacional:

 - Category
	 - iD : Chave primária da entidade (int)
	 - Title: Título da categoria (string)
	 - ProductCategories : Coleção da tabela many to many (ICollection<ProductCategory>)
 - Product
	 - iD : Chave primária da entidade (int)
	 - Title: Título do produto (string)
	 - Description: Descrição do produto (string)
	 - Price: Preço do produto (decimal)
	 - ProductCategories : Coleção da tabela many to many (ICollection<ProductCategory>)
	
 - ProductCategory
	 - iD : Chave primária da entidade (int)
	 - ProductId : Id do produto relacionado (int)
	 - CategoryId : Id da categoria relacionada (int)
	 
# Funções
O AspNetCoreAPI consiste em algumas funções, atendendo ao CRUD:

 - Adicionar Categoria
 - Listar Categorias e os produtos que são relacionados a elas
 - Adicionar Produto
 - Listar Produtos e as categorias que são relacionadas a eles
 - Listar Produto pelo ID
 - Listar Produtos pelo ID da categoria
 - Atualizar Produto especificado pelo ID
 - Deletar Produto especificado pelo ID

# Conceitos aplicados
No desenvolvimento do AspNetCoreAPI alguns conceitos foram colocados em prática:
	
 - ASP.NET CORE MVC 3.1 API
 	- Sistema de roteamento por controllers e por métodos HTTP
	- Validações com base em Model
	- Padrão REST API
- CLI do dotnet
```dotnet
dotnet new webapi -o aspnetcoreapi
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.5
dotnet run
```
 - Entity Framework (code-first)
 	- Conceitos many to many (muitos para muitos) na relação do banco de dados
 	- Migrations para construção do banco de dados (pasta Migrations)
 - Conexão ao banco de dados utilizando SQL Server em nuvem (Senha da connection string já modificada no servidor)
 - Conceito MVC
 	- Models de entidade (Pasta Models)
 	- Controladores de entidade (Pasta Controlers)
 - CRUD utilizando um ORM (Entity Framework)
 - Documentação da API feita no Insomnia (arquivo Insomnia_2020-07-12.json)
 
 
 <img src="https://user-images.githubusercontent.com/62113721/87360043-944e6a80-c53f-11ea-92b7-0a59f8cd7d3d.png" 
width="95%" height="400px"
/> 
 <img src="https://user-images.githubusercontent.com/62113721/87360827-4a668400-c541-11ea-82d5-736a0c5966e8.png" 
width="95%" height="400px"
/> 



