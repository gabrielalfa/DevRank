# DevRank

DevRank é um protótipo funcional de uma plataforma de avaliação técnica, gamificação competitiva e evolução profissional para desenvolvedores.

O objetivo não é ser um produto final. O objetivo é validar conceito, experiência, engajamento e percepção de valor.

## Visão

A proposta do DevRank é ajudar desenvolvedores a descobrirem seu nível real por meio de desafios práticos, ranking Elo, feedback fake de IA e perfis técnicos compartilháveis.

A plataforma foi pensada para não parecer uma prova acadêmica, quiz escolar ou LeetCode genérico. A experiência busca simular um ambiente mais próximo de engenharia real:

- desafios de mercado;
- debugging;
- refatoração;
- performance;
- comunicação técnica;
- entrevistas simuladas;
- maturidade profissional;
- competição saudável;
- evolução contínua.

## Stack

- ASP.NET MVC 5
- .NET Framework 4.8
- Razor
- Bootstrap 5 como referência visual
- JavaScript Vanilla
- CSS customizado
- Visual Studio 2022

## Restrições do protótipo

Este projeto foi criado propositalmente sem arquitetura pesada.

Não usa:

- SQL Server;
- Entity Framework;
- APIs externas;
- Docker;
- Azure;
- Redis;
- JWT;
- OAuth;
- microserviços.

Os dados ficam em memória usando `FakeDatabase`.

## Módulos implementados

### Home Premium

A Home foi evoluída para vender melhor a ideia do produto:

- hero forte com proposta de valor;
- prova social fake;
- ranking ao vivo fake;
- preview de desafio real;
- comparativo com testes comuns;
- timeline de evolução;
- depoimentos fake;
- CTA para criar perfil e entrar na arena.

### Header Mobile Premium

Foi criado um header responsivo com:

- menu mobile lateral/offcanvas;
- botão hamburguer;
- scrim;
- transições suaves;
- navegação adaptada para mobile.

### Footer Tornfy

Footer profissional com marca Tornfy:

- descrição da plataforma;
- links de produto;
- ecossistema fake;
- links institucionais;
- links legais;
- redes sociais fake.

### Cadastro e Login Simples

Fluxo simples para protótipo:

- `AccountController`;
- login com usuário e senha fake;
- cadastro de novo programador;
- sessão usando `Session["UserId"]`.

Usuário inicial para teste:

```text
usuário: ana
senha: 123
```

### Dashboard do Dev

Painel de evolução profissional com:

- Elo geral;
- rank;
- win streak;
- winrate;
- coach insight fake;
- mapa de skills;
- desafios recomendados;
- evolução semanal fake;
- histórico recente.

### Perfil Técnico Avançado

Perfil enriquecido com:

- nome;
- username;
- avatar fake local;
- stack principal;
- stack secundária;
- senioridade percebida;
- experiência;
- badges;
- Elo geral;
- Elo por tecnologia;
- skills técnicas e humanas;
- integrações fake;
- histórico de partidas.

### Perfil Público Compartilhável

Página pública por username:

```text
/Profile/Public?username=ana
```

Inclui:

- identidade técnica pública;
- botão copiar link;
- Elo;
- rank;
- winrate;
- badges;
- cards de skill;
- evolução fake.

### Arena de Desafios

Lista de desafios fake com cenários como:

- refatorar endpoint MVC;
- corrigir query lenta;
- detectar bug de concorrência;
- melhorar arquitetura de service;
- resolver problema de performance;
- revisar código legado.

### Avaliação Fake por IA

`FakeAIService` gera:

- score controlado;
- feedback técnico fake;
- aprovação ou reprovação;
- alteração de Elo.

Hoje essa avaliação é fake e heurística. Ela existe para validar fluxo de produto, não para substituir avaliação técnica real.

## Estratégia de Curadoria e IA

O ponto fraco natural de uma plataforma como essa é a curadoria. O fundador não deve ser o gargalo de revisão.

Por isso, o projeto passou a considerar um modelo de:

- homologação comunitária;
- reputação separada do Elo técnico;
- níveis de confiança;
- revisão distribuída;
- qualidade por votos;
- moderação fake;
- IA futura como apoio, não como juiz absoluto.

No momento, nenhuma API de IA é chamada e nenhum token é consumido.

A direção futura está documentada em:

```text
docs/PRODUCT_STRATEGY.md
```

### Community Hub

Módulo criado para validar contribuição controlada da comunidade:

- criação de desafios;
- fila de homologação;
- painel de revisão;
- níveis comunitários;
- reputação comunitária;
- badges;
- ranking de curadores;
- quality gate fake;
- estratégia de financiamento futuro para tokens.

Níveis comunitários:

- Contributor;
- Trusted Contributor;
- Technical Reviewer;
- Senior Curator;
- Arena Architect.

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

1. Abra a aplicação.
2. Crie um perfil ou use `ana / 123`.
3. Acesse o Dashboard.
4. Entre na Arena.
5. Abra um desafio.
6. Escreva uma solução.
7. Envie para a IA fake.
8. Veja o resultado e o Elo atualizado.
9. Acesse o Perfil.
10. Abra o Perfil Público.

## Observações importantes

Como o armazenamento é em memória, novos usuários, histórico e partidas são perdidos quando a aplicação reinicia.

Isso é intencional para manter o projeto simples, rápido e focado em validação de experiência.

## Status

Protótipo em evolução.

Próximos módulos sugeridos:

- perfil editável;
- simulador de entrevista;
- modo treinamento sem punição;
- sistema anti-copy fake;
- feedback inteligente mais rico por rubricas;
- homologação comunitária mais profunda;
- política futura de uso de IA com créditos;
- estatísticas de skill mais detalhadas;
- match futuro de vagas.

## Empresa fictícia

DevRank foi apresentado como um produto do ecossistema Tornfy para dar mais credibilidade visual ao protótipo.
