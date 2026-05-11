# DevRank

DevRank e um prototipo funcional de uma plataforma de avaliacao tecnica, gamificacao competitiva e evolucao profissional para desenvolvedores.

O objetivo nao e ser um produto final. O objetivo e validar conceito, experiencia, engajamento e percepcao de valor.

## Visao

A proposta do DevRank e ajudar desenvolvedores a descobrirem seu nivel real por meio de desafios praticos, ranking Elo, feedback fake de IA e perfis tecnicos compartilhaveis.

A plataforma foi pensada para nao parecer uma prova academica, quiz escolar ou LeetCode generico. A experiencia busca simular um ambiente mais proximo de engenharia real:

- desafios de mercado;
- debugging;
- refatoracao;
- performance;
- comunicacao tecnica;
- entrevistas simuladas;
- maturidade profissional;
- competicao saudavel;
- evolucao continua.

## Stack

- ASP.NET MVC 5
- .NET Framework 4.8
- Razor
- Bootstrap 5 como referencia visual
- JavaScript Vanilla
- CSS customizado
- Visual Studio 2022

## Restricoes do prototipo

Este projeto foi criado propositalmente sem arquitetura pesada.

Nao usa:

- SQL Server;
- Entity Framework;
- APIs externas;
- Docker;
- Azure;
- Redis;
- JWT;
- OAuth;
- microservicos.

Os dados ficam em memoria usando `FakeDatabase`.

## Modulos implementados

### Home Premium

A Home foi evoluida para vender melhor a ideia do produto:

- hero forte com proposta de valor;
- prova social fake;
- ranking ao vivo fake;
- preview de desafio real;
- comparativo com testes comuns;
- timeline de evolucao;
- depoimentos fake;
- CTA para criar perfil e entrar na arena.

### Header Mobile Premium

Foi criado um header responsivo com:

- menu mobile lateral/offcanvas;
- botao hamburguer;
- scrim;
- transicoes suaves;
- navegacao adaptada para mobile.

### Footer Tornfy

Footer profissional com marca Tornfy:

- descricao da plataforma;
- links de produto;
- ecossistema fake;
- links institucionais;
- links legais;
- redes sociais fake.

### Cadastro e Login Simples

Fluxo simples para prototipo:

- `AccountController`;
- login com usuario e senha fake;
- cadastro de novo programador;
- sessao usando `Session["UserId"]`.

Usuario inicial para teste:

```text
usuario: ana
senha: 123
```

### Dashboard do Dev

Painel de evolucao profissional com:

- Elo geral;
- rank;
- win streak;
- winrate;
- coach insight fake;
- mapa de skills;
- desafios recomendados;
- evolucao semanal fake;
- historico recente.

### Perfil Tecnico Avancado

Perfil enriquecido com:

- nome;
- username;
- avatar fake local;
- stack principal;
- stack secundaria;
- senioridade percebida;
- experiencia;
- badges;
- Elo geral;
- Elo por tecnologia;
- skills tecnicas e humanas;
- integracoes fake;
- historico de partidas.

### Perfil Publico Compartilhavel

Pagina publica por username:

```text
/Profile/Public?username=ana
```

Inclui:

- identidade tecnica publica;
- botao copiar link;
- Elo;
- rank;
- winrate;
- badges;
- cards de skill;
- evolucao fake.

### Arena de Desafios

Lista de desafios fake com cenarios como:

- refatorar endpoint MVC;
- corrigir query lenta;
- detectar bug de concorrencia;
- melhorar arquitetura de service;
- resolver problema de performance;
- revisar codigo legado.

### Avaliacao Fake por IA

`FakeAIService` gera:

- score controlado;
- feedback tecnico fake;
- aprovacao ou reprovacao;
- alteracao de Elo.

### Ranking Elo

`EloService` calcula rating e define ranks:

- Bronze;
- Prata;
- Ouro;
- Platina;
- Diamante;
- Mestre;
- Challenger.

## Estrutura do projeto

```text
DevRank/
  App_Start/
  Controllers/
  Content/
  FakeDatabase/
  Models/
  Scripts/
  Services/
  Views/
```

## Como executar

1. Abra `DevRank.sln` no Visual Studio 2022.
2. Restaure os pacotes NuGet, se o Visual Studio solicitar.
3. Defina `DevRank` como projeto inicial.
4. Execute com `F5`.
5. Acesse a Home e teste o fluxo.

## Fluxo sugerido de teste

1. Abra a aplicacao.
2. Crie um perfil ou use `ana / 123`.
3. Acesse o Dashboard.
4. Entre na Arena.
5. Abra um desafio.
6. Escreva uma solucao.
7. Envie para a IA fake.
8. Veja o resultado e o Elo atualizado.
9. Acesse o Perfil.
10. Abra o Perfil Publico.

## Observacoes importantes

Como o armazenamento e em memoria, novos usuarios, historico e partidas sao perdidos quando a aplicacao reinicia.

Isso e intencional para manter o projeto simples, rapido e focado em validacao de experiencia.

## Status

Prototipo em evolucao.

Proximos modulos sugeridos:

- perfil editavel;
- simulador de entrevista;
- modo treinamento sem punicao;
- sistema anti-copy fake;
- feedback inteligente mais rico;
- estatisticas de skill mais detalhadas;
- match futuro de vagas.

## Empresa ficticia

DevRank foi apresentado como um produto do ecossistema Tornfy para dar mais credibilidade visual ao prototipo.
