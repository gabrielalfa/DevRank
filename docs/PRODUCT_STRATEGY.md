# DevRank - Estratégia de Curadoria Comunitária e IA Futura

Este documento registra a direção de produto do DevRank após a validação inicial do protótipo.

## Problema Central

A avaliação técnica de qualidade depende, hoje, de julgamento humano:

- um bom problema precisa parecer real;
- uma boa resposta precisa considerar contexto, trade-offs e comunicação;
- respostas superficiais podem parecer corretas para avaliadores fracos;
- desafios ruins destroem a confiança da plataforma.

O fundador não deve ser gargalo de curadoria.

## Direção do Produto

O DevRank deve evoluir para um sistema onde a própria comunidade ajuda a:

- propor desafios;
- revisar critérios;
- homologar problemas;
- validar respostas;
- denunciar baixa qualidade;
- elevar conteúdos úteis;
- reduzir spam e farming.

A curadoria deve ser distribuída, mas controlada por reputação.

## Princípio

> Nenhum conteúdo comunitário vira oficial sem homologação.

## Homologação Comunitária

Todo desafio criado pela comunidade passa por etapas:

1. **Draft**
   O dev cria o desafio.

2. **Shadow Review**
   O sistema fake verifica tamanho, superficialidade, repetição e sinais genéricos.

3. **Community Review**
   Revisores com reputação mínima avaliam clareza, relevância, dificuldade e resposta esperada.

4. **Homologado**
   O desafio atinge votos e confiança suficientes para entrar na arena.

5. **Monitoramento**
   Taxa de abandono, denúncias e feedbacks podem rebaixar ou ocultar o desafio.

## Reputação Comunitária

Separada do Elo técnico.

Ganha reputação por:

- desafio homologado;
- revisão útil;
- feedback aceito;
- denúncia confirmada;
- melhoria aprovada;
- resposta marcada como útil.

Perde reputação por:

- spam;
- conteúdo genérico;
- plágio;
- revisão ruim;
- denúncia procedente;
- tentativa de farming.

## IA: Não Agora, Mas Preparado

No momento, o projeto não deve consumir tokens.

Motivos:

- custo;
- produto ainda em validação;
- rubricas ainda imaturas;
- risco de gastar antes de provar retenção.

Porém, o sistema já deve nascer com uma espera conceitual para IA futura.

## Estratégia Futura de IA

IA não será juiz absoluto.

Uso previsto:

- triagem inicial;
- detecção de resposta genérica;
- análise por rubrica;
- feedback formativo;
- perguntas de follow-up;
- sinalização de baixa confiança;
- apoio aos revisores humanos.

## Modelo Híbrido Futuro

```text
Resposta do dev
  -> avaliação heurística local
  -> avaliação por rubrica
  -> IA futura quando houver crédito/token
  -> revisão comunitária quando confiança for baixa
  -> resultado final com score + confiança
```

## Como Financiar Tokens no Futuro

A plataforma deve buscar se pagar por:

- planos premium para devs;
- empresas observando rankings;
- créditos de avaliação;
- patrocínio de challenges;
- assinatura para times;
- marketplace de trilhas;
- contribuição da comunidade;
- limites gratuitos com upgrade.

## Decisão Técnica Atual

Por enquanto:

- `FakeDatabase` continua como padrão;
- `FakeAIService` continua local/fake;
- criar modelos de política de IA futura;
- exibir status "IA futura desativada";
- simular orçamento de tokens;
- preparar pontos de extensão sem chamar API externa.

## Regra de Produto

O DevRank não deve prometer:

> "IA substitui avaliadores."

Deve prometer:

> "Comunidade, reputação e IA assistida tornam avaliação técnica mais contínua, comparável e escalável."

