# Sobre o BookListMVC

O BookListMVC é um simples gerenciador de livros feito em **ASP.NET CORE 2.1** utilizando o **Entity Framework** como ORM, o projeto foi feito para fins de aprendizado. Conceitos como requisições a API, Razor Pages, validações com base no modelo, padrão de CodeFirst, Ajax, Frameworks de webdesign e SQLite foram aplicados de forma prática nesta solução.

<img src="https://user-images.githubusercontent.com/62113721/87240688-2fd1c500-c3f2-11ea-9956-a804738eef91.gif" 
width="95%" height="614px"
/> 

# Entidades
O note_app consiste em uma entidade de livros, onde possui a seguinte estrutura:

 - NoteSet
	 - iD : Chave primária da entidade (int)
	 - Name: Nome do livro (string)
	 - Author: Autor do livro (string)
	 - ISBN: Sistema internacional de identificação de livros (DateTime)

# Funções
O BookListMVC consiste em 4 funções, atendendo ao CRUD:

 - Adicionar novo livro
 - Listar livros
 - Editar livro específico
 - Remover livro específico

# Conceitos aplicados
No desenvolvimento do BookListMVC alguns conceitos foram colocados em prática:
	
 - ASP.NET CORE MVC 2.1
 	- Sistema de roteamento
	- Requisições para a API (monolítica)
	- Razor Pages
	- Utilização de AJAX para páginas dinâmicas

 - Frameworks de Web Design
 	- Data Tables (para criar tabelas interativas)
	- Sweet Alert (Alertas mais trabalhados e bonitos)
	- Toastr (Notificações Toast que mostram mensagens de sucesso ou erro vindas da api para o usuário)
	- Bootstrap (Biblioteca Html/CSS e Javascript para criação de layouts responsivos)
 - Entity Framework code-first)
 - Conexão ao banco de dados utilizando SQLITE (arquivo Database.db)
 - Conceito MVC
 	- Models de entidade (Pasta Models)
	- Views de entidade (Pasta Views)
 	- Controladores de entidade (Pasta Controls)
 - CRUD utilizando um ORM (Entity Framework)

<img src="https://user-images.githubusercontent.com/62113721/87240683-28122080-c3f2-11ea-925b-c18034e2584d.gif" 
width="95%" height="614px"
/> 

<img src="https://user-images.githubusercontent.com/62113721/87240677-16c91400-c3f2-11ea-9116-c8ea2f6f86cd.gif" 
width="95%" height="614px"
/> 

