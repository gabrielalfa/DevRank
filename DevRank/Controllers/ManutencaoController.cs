using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DevRank.Models;
using Db = DevRank.Data.AppData;

namespace DevRank.Controllers
{
    public class ManutencaoController : Controller
    {
        public ActionResult CriarDesafiosManual()
        {
            if ((Request.QueryString["confirmar"] ?? string.Empty).ToLower() != "sim")
            {
                return Content("Seed de manutenção bloqueado. Para executar, acesse /Manutencao/CriarDesafiosManual?confirmar=sim", "text/plain", Encoding.UTF8);
            }

            var challenges = BuildManualChallenges();
            var inserted = Db.CreateManualChallenges(challenges);

            var response = new StringBuilder();
            response.AppendLine("Seed manual de desafios DevRank");
            response.AppendLine("Criados: " + inserted);
            response.AppendLine("Já existiam: " + (challenges.Count - inserted));
            response.AppendLine("Total enviado: " + challenges.Count);
            response.AppendLine();
            response.AppendLine("Desafios preparados:");

            foreach (var challenge in challenges.OrderBy(item => item.ChallengeRating))
            {
                response.AppendLine("- " + challenge.Title + " [" + challenge.Category + " / " + challenge.Difficulty + " / " + challenge.ChallengeRating + " Elo]");
            }

            return Content(response.ToString(), "text/plain", Encoding.UTF8);
        }

        private static List<Challenge> BuildManualChallenges()
        {
            return new List<Challenge>
            {
                Challenge("Controller com regra fiscal escondida", "Um controller MVC passou a calcular desconto fiscal diretamente na action para resolver uma urgência. Avalie o risco, proponha uma extração incremental e explique como proteger o comportamento sem criar arquitetura exagerada.", "Médio", "ASP.NET MVC", 950, "25 min", 1320,
@"public ActionResult Checkout(OrderViewModel model)
{
    if (model.State == ""SP"")
    {
        model.Total = model.Total - (model.Total * 0.12m);
    }

    _orderRepository.Save(model.ToOrder());
    return RedirectToAction(""Success"");
}"),
                Challenge("Query N+1 em tela de pedidos", "A tela funciona em homologação, mas em produção trava quando clientes com muitos pedidos acessam o relatório. Identifique o problema, proponha uma consulta/projeção melhor e diga como mediria o ganho.", "Médio", "SQL / Performance", 1000, "25 min", 1420,
@"var customers = db.Customers.ToList();
foreach (var customer in customers)
{
    customer.LastOrder = db.Orders
        .Where(order => order.CustomerId == customer.Id)
        .OrderByDescending(order => order.CreatedAt)
        .FirstOrDefault();
}"),
                Challenge("Cache causando dado errado", "Um cache foi adicionado para reduzir carga, mas usuários começaram a ver informações desatualizadas após alteração de cadastro. Explique invalidação, escopo de chave e risco de cache compartilhado.", "Médio", "Backend", 1050, "25 min", 1450,
@"public Customer GetCustomer(int id)
{
    var key = ""customer"" + id;
    if (MemoryCache.Default[key] != null)
        return (Customer)MemoryCache.Default[key];

    var customer = _repository.Get(id);
    MemoryCache.Default[key] = customer;
    return customer;
}"),
                Challenge("Deadlock em atualização de estoque", "Dois pedidos simultâneos podem reservar o mesmo estoque. Mostre onde está a condição de corrida e proponha uma solução pragmática com transação, lock otimista ou operação atômica.", "Difícil", "Concorrência", 1300, "35 min", 1780,
@"var product = repository.Get(productId);
if (product.Stock >= quantity)
{
    product.Stock -= quantity;
    repository.Save(product);
    return true;
}
return false;"),
                Challenge("Action assíncrona falsa", "Uma action usa async, mas continua bloqueando thread com chamadas síncronas e Result. Explique o problema e refatore para um fluxo assíncrono coerente.", "Médio", ".NET", 1100, "25 min", 1500,
@"public async Task<ActionResult> Details(int id)
{
    var user = _userService.GetAsync(id).Result;
    var orders = _orderService.GetOrders(user.Id);
    return View(new UserDetails(user, orders));
}"),
                Challenge("Upload sem validação segura", "Um endpoint aceita upload de currículo, mas confia no nome e extensão enviados pelo usuário. Identifique riscos e proponha validações mínimas para um protótipo seguro.", "Médio", "Segurança", 1150, "25 min", 1520,
@"[HttpPost]
public ActionResult Upload(HttpPostedFileBase file)
{
    var path = Server.MapPath(""~/Uploads/"" + file.FileName);
    file.SaveAs(path);
    return RedirectToAction(""Index"");
}"),
                Challenge("View Razor com regra demais", "Uma view Razor começou a calcular status, formatar regras e decidir visibilidade. Proponha uma limpeza simples preservando MVC 5 e sem trazer framework novo.", "Fácil", "Razor / UX", 800, "20 min", 1180,
@"@foreach (var item in Model.Orders)
{
    var overdue = item.DueDate < DateTime.Now && item.Status != ""Paid"";
    <tr class=""@(overdue ? ""danger"" : """")"">
        <td>@item.Customer.Name</td>
        <td>@(overdue ? ""Atrasado"" : item.Status)</td>
    </tr>
}"),
                Challenge("JavaScript duplicando evento", "Depois de navegar por abas, o botão salvar dispara duas ou três requisições. Identifique a causa provável e proponha uma correção com JavaScript puro.", "Médio", "JavaScript", 900, "20 min", 1300,
@"function initSave() {
    document.querySelector('#save').addEventListener('click', function () {
        fetch('/orders/save', { method: 'POST' });
    });
}

initSave();
initSave();"),
                Challenge("Service com responsabilidades misturadas", "Um service valida entrada, salva no banco, envia e-mail e escreve auditoria. Quebre responsabilidades sem criar Clean Architecture artificial.", "Médio", "Arquitetura", 1000, "30 min", 1480,
@"public void ApproveInvoice(Invoice invoice)
{
    Validate(invoice);
    _repository.Save(invoice);
    _email.Send(invoice.CustomerEmail);
    File.AppendAllText(""audit.log"", invoice.Id.ToString());
}"),
                Challenge("Erro engolido em produção", "Um catch vazio resolveu o erro visualmente, mas agora o time não consegue diagnosticar falhas intermitentes. Explique o impacto e reescreva a estratégia de tratamento.", "Fácil", "Debugging", 750, "20 min", 1160,
@"try
{
    paymentGateway.Charge(order);
}
catch
{
}

return RedirectToAction(""Thanks"");"),
                Challenge("Filtro de busca carregando tudo", "A busca carrega todos os registros em memória e depois filtra. Proponha uma versão que filtre, ordene e pagine no banco.", "Fácil", "Performance", 850, "20 min", 1240,
@"var products = db.Products.ToList();
if (!string.IsNullOrEmpty(term))
{
    products = products.Where(p => p.Name.Contains(term)).ToList();
}
return View(products.Take(50));"),
                Challenge("Sessão usada como carrinho permanente", "O carrinho depende apenas de Session e usuários reclamam que perdem compras ao trocar de dispositivo. Explique limites de sessão e sugira evolução simples.", "Médio", "ASP.NET MVC", 900, "25 min", 1360,
@"public ActionResult Add(int productId)
{
    var cart = Session[""Cart""] as List<int> ?? new List<int>();
    cart.Add(productId);
    Session[""Cart""] = cart;
    return RedirectToAction(""Index"");
}"),
                Challenge("Permissão conferida só na tela", "O botão de excluir some para usuários comuns, mas a action continua acessível por URL direta. Explique a falha e proponha proteção no servidor.", "Médio", "Segurança", 1000, "20 min", 1440,
@"@if (Model.CanDelete)
{
    <a href=""/users/delete/@Model.Id"">Excluir</a>
}

public ActionResult Delete(int id)
{
    _users.Delete(id);
    return RedirectToAction(""Index"");
}"),
                Challenge("Integração externa sem timeout", "Uma chamada HTTP sem timeout trava requisições em cascata quando o fornecedor fica lento. Proponha timeout, fallback e observabilidade mínima.", "Difícil", "APIs", 1250, "30 min", 1680,
@"public string GetScore(string document)
{
    var client = new WebClient();
    return client.DownloadString(""https://vendor/score?doc="" + document);
}"),
                Challenge("Migration manual quebrando deploy", "Um deploy depende de rodar SQL manual em produção. Desenhe um processo simples para reduzir risco em hospedagem compartilhada e protótipo MVC 5.", "Médio", "DevOps", 1050, "25 min", 1460,
@"-- Executado manualmente antes do deploy
ALTER TABLE programmers ADD COLUMN github VARCHAR(120);
UPDATE programmers SET github = '';
-- Aplicação nova assume que a coluna existe"),
                Challenge("Model binding aceitando campo indevido", "Um formulário permite alterar apenas nome, mas o usuário consegue enviar IsAdmin no POST. Explique overposting e corrija com ViewModel ou bind explícito.", "Médio", "ASP.NET MVC", 1100, "25 min", 1550,
@"[HttpPost]
public ActionResult Edit(User user)
{
    db.Entry(user).State = EntityState.Modified;
    db.SaveChanges();
    return RedirectToAction(""Index"");
}"),
                Challenge("Relatório mensal sem índice", "Uma consulta filtra por data e cliente em uma tabela grande. Explique índice composto, seletividade e como validar plano de execução.", "Difícil", "SQL", 1300, "35 min", 1760,
@"SELECT *
FROM Orders
WHERE CustomerId = @customerId
  AND CreatedAt BETWEEN @start AND @end
ORDER BY CreatedAt DESC"),
                Challenge("Tela sem estado de loading", "Usuários clicam várias vezes porque não há feedback visual durante requisição. Crie uma solução simples com estado disabled, loading e prevenção de duplo envio.", "Fácil", "Frontend", 750, "20 min", 1140,
@"document.querySelector('#pay').addEventListener('click', function () {
    fetch('/payment/pay', { method: 'POST' })
        .then(function () { window.location = '/success'; });
});"),
                Challenge("Log com dado sensível", "Logs estão ajudando no suporte, mas começaram a gravar CPF, token e payload completo. Explique risco e proponha sanitização.", "Médio", "Segurança", 1150, "25 min", 1580,
@"public void LogRequest(LoginRequest request)
{
    File.AppendAllText(""log.txt"", JsonConvert.SerializeObject(request));
}"),
                Challenge("Código legado sem teste de caracterização", "Antes de refatorar uma regra crítica, o time não sabe o comportamento esperado. Explique teste de caracterização e faça um plano incremental.", "Médio", "Legado", 950, "25 min", 1380,
@"public decimal Calc(int type, decimal value)
{
    if (type == 1) return value * 0.91m;
    if (type == 2 && value > 1000) return value - 180;
    if (type == 3) return value + 25;
    return value;
}"),
                Challenge("Entrevista: explique trade-off técnico", "Você precisa justificar por que não vai reescrever um sistema legado agora. Responda com clareza técnica, risco, custo e alternativa incremental.", "Médio", "Comunicação", 900, "20 min", 1340,
@"Contexto: sistema MVC 5 legado, faturamento mensal crítico, time pequeno e prazo de 3 semanas.
Pergunta: por que refatorar incrementalmente em vez de reescrever tudo?"),
                Challenge("Incidente: comunicação sob pressão", "A API de pagamento está instável e o cliente exige previsão. Escreva uma resposta técnica e humana, sem prometer o que não sabe.", "Difícil", "Gestão de crise", 1200, "25 min", 1620,
@"Sintomas:
- taxa de erro subiu de 0,5% para 9%
- fornecedor externo com latência alta
- suporte recebendo reclamações
- diretoria pedindo ETA"),
                Challenge("PR grande demais para revisar", "Um pull request mistura refatoração, mudança visual e regra de negócio. Diga como revisaria, que perguntas faria e como reduziria risco.", "Médio", "Code Review", 950, "25 min", 1400,
@"PR:
- altera 38 arquivos
- muda regra de desconto
- troca layout do checkout
- renomeia services
- remove testes antigos"),
                Challenge("Feature flag mal removida", "Uma feature flag antiga deixou dois fluxos vivos e ninguém sabe qual é o correto. Proponha limpeza segura com métrica, logs e plano de remoção.", "Difícil", "Arquitetura", 1250, "30 min", 1700,
@"if (FeatureFlags.NewBilling)
{
    return _newBilling.Charge(invoice);
}

return _legacyBilling.Charge(invoice);"),
                Challenge("Repository retornando IQueryable para a View", "A camada de acesso vaza IQueryable até a tela e cada alteração na View muda a consulta executada. Explique o acoplamento e proponha uma fronteira mais previsível.", "Médio", "Arquitetura", 1050, "25 min", 1490,
@"public IQueryable<Order> GetOrders()
{
    return _context.Orders;
}

public ActionResult Index()
{
    return View(_repository.GetOrders());
}"),
                Challenge("Filtro global escondendo erro de autorização", "Um filtro global redireciona qualquer exceção para a Home. Usuários sem permissão veem tela vazia e o suporte perde diagnóstico. Refaça a estratégia.", "Médio", "ASP.NET MVC", 950, "25 min", 1390,
@"public class ErrorFilter : HandleErrorAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.Result = new RedirectResult(""/"");
        context.ExceptionHandled = true;
    }
}"),
                Challenge("Validação duplicada no front e no back", "A mesma regra de limite de crédito existe em JavaScript e no controller, mas divergiu. Explique onde a regra deve morar e como manter UX rápida sem duplicação perigosa.", "Médio", "Arquitetura", 1000, "25 min", 1430,
@"if (amount > 5000) {
    alert('Valor acima do limite');
}

if (model.Amount > 7000)
{
    ModelState.AddModelError(""Amount"", ""Valor acima do limite"");
}"),
                Challenge("Partial view com consulta ao banco", "Uma partial view acessa serviço diretamente para montar menu. A tela ficou lenta e difícil de testar. Proponha uma solução MVC 5 simples.", "Médio", "Razor / MVC", 900, "20 min", 1310,
@"@{
    var notifications = DependencyResolver.Current
        .GetService<NotificationService>()
        .GetUnread(User.Identity.Name);
}
<span>@notifications.Count</span>"),
                Challenge("Paginação instável com dados novos", "Usuários relatam itens duplicados entre páginas quando novos registros entram durante a navegação. Explique ordenação determinística e alternativas.", "Difícil", "SQL / Performance", 1250, "30 min", 1660,
@"SELECT *
FROM Events
ORDER BY CreatedAt DESC
LIMIT 20 OFFSET 40"),
                Challenge("Transação longa segurando lock", "Um processo abre transação, chama API externa e só depois confirma o banco. Em pico, a aplicação fica travada. Identifique o risco e redesenhe o fluxo.", "Difícil", "Concorrência", 1350, "35 min", 1820,
@"using (var tx = db.BeginTransaction())
{
    SaveInvoice(invoice);
    paymentGateway.Charge(invoice);
    tx.Commit();
}"),
                Challenge("Configuração sensível no Web.config versionado", "A connection string real foi commitada no repositório. Explique riscos e proponha uma estratégia prática para dev, homologação e produção.", "Médio", "Segurança", 950, "20 min", 1370,
@"<connectionStrings>
  <add name=""DevRankDb""
       connectionString=""Server=prod;Database=app;Uid=root;Pwd=123456;"" />
</connectionStrings>"),
                Challenge("Controller inchado com fluxo de checkout", "O checkout cresceu e agora a action tem validação, cálculo de frete, cupom, pagamento e e-mail. Proponha uma refatoração incremental testável.", "Difícil", "ASP.NET MVC", 1300, "35 min", 1740,
@"public ActionResult Pay(CheckoutViewModel model)
{
    Validate(model);
    ApplyCoupon(model);
    CalculateShipping(model);
    ChargeCard(model);
    SendReceipt(model.Email);
    return RedirectToAction(""Done"");
}"),
                Challenge("Dropdown carregando 20 mil opções", "Uma tela de cadastro carrega todos os clientes em um select. No mobile fica inutilizável. Proponha busca incremental e limites de UX.", "Fácil", "Frontend", 800, "20 min", 1200,
@"<select id=""CustomerId"">
@foreach (var customer in Model.AllCustomers)
{
    <option value=""@customer.Id"">@customer.Name</option>
}
</select>"),
                Challenge("Exportação CSV quebrando memória", "O relatório exporta tudo em memória antes de devolver o arquivo. Em produção, o worker reinicia. Explique streaming, paginação e limites.", "Difícil", "Performance", 1300, "35 min", 1750,
@"var rows = repository.GetAll();
var csv = new StringBuilder();
foreach (var row in rows)
{
    csv.AppendLine(row.ToCsv());
}
return File(Encoding.UTF8.GetBytes(csv.ToString()), ""text/csv"");"),
                Challenge("Erro intermitente por DateTime.Now", "Testes e regras de vencimento falham dependendo do horário e fuso. Explique como isolar relógio e melhorar previsibilidade.", "Médio", ".NET", 900, "20 min", 1280,
@"public bool IsExpired(DateTime dueDate)
{
    return dueDate < DateTime.Now;
}"),
                Challenge("Busca com SQL concatenado", "Uma busca rápida foi feita concatenando termo do usuário no SQL. Identifique a vulnerabilidade e reescreva com parâmetros.", "Fácil", "Segurança / SQL", 850, "20 min", 1260,
@"var sql = ""SELECT * FROM Customers WHERE Name LIKE '%"" + term + ""%'"";
var customers = db.Database.SqlQuery<Customer>(sql).ToList();"),
                Challenge("Fila fake com Thread.Sleep", "Uma rotina simula retentativa com Thread.Sleep dentro da requisição. Explique impacto no ASP.NET e proponha alternativa simples.", "Médio", ".NET", 1050, "25 min", 1470,
@"for (var attempt = 0; attempt < 3; attempt++)
{
    if (TrySendEmail(message)) break;
    Thread.Sleep(5000);
}"),
                Challenge("Retentativa duplicando cobrança", "Uma chamada de pagamento usa retry automático, mas não há idempotência. Explique como evitar cobrança duplicada.", "Difícil", "APIs / Pagamentos", 1350, "35 min", 1840,
@"public void Pay(Order order)
{
    retry.Execute(() => gateway.Charge(order.Total, order.CardToken));
    order.MarkAsPaid();
}"),
                Challenge("Deploy com arquivo antigo no bin", "Depois de publicar via FTP, a aplicação usa uma DLL antiga e comportamento mistura versões. Crie um checklist de deploy seguro para hospedagem compartilhada.", "Médio", "DevOps", 900, "20 min", 1330,
@"Publicação manual:
1. enviar Views
2. enviar bin
3. testar Home
Problema: arquivos removidos localmente continuam no servidor."),
                Challenge("Leaderboard recalculado em toda requisição", "O ranking ordena milhares de usuários em toda abertura da Home. Explique cache, invalidação e pré-cálculo sem Redis.", "Médio", "Performance", 1100, "25 min", 1530,
@"public ActionResult Index()
{
    var top = db.Programmers
        .OrderByDescending(p => p.EloRating)
        .Take(50)
        .ToList();
    return View(top);
}"),
                Challenge("ViewModel gigante para todas as telas", "Um único ViewModel alimenta Home, Perfil e Dashboard. Mudanças simples quebram telas não relacionadas. Proponha separação pragmática.", "Médio", "Arquitetura", 950, "25 min", 1360,
@"public class AppViewModel
{
    public HomeData Home { get; set; }
    public ProfileData Profile { get; set; }
    public DashboardData Dashboard { get; set; }
    public AdminData Admin { get; set; }
}"),
                Challenge("Exceção de null mascarando regra de negócio", "Um NullReferenceException acontece quando cliente não tem endereço. O time quer só usar ?. Explique quando isso mascara domínio e proponha validação correta.", "Fácil", "Debugging", 750, "20 min", 1150,
@"var city = order.Customer.Address.City;
shipping.Calculate(city, order.Total);"),
                Challenge("Endpoint sem limite de tamanho", "Um campo de bio aceita qualquer tamanho e usuários conseguem enviar payload enorme. Proponha validação no model, request e banco.", "Médio", "Segurança", 900, "20 min", 1320,
@"[HttpPost]
public ActionResult SaveBio(string bio)
{
    profile.Bio = bio;
    repository.Save(profile);
    return RedirectToAction(""Index"");
}"),
                Challenge("Ordenação feita no JavaScript após paginação", "A API retorna página de 20 itens e o front ordena só esses 20. O usuário acha que está vendo ranking global. Corrija a responsabilidade.", "Médio", "Frontend / APIs", 950, "25 min", 1380,
@"fetch('/api/players?page=2')
    .then(r => r.json())
    .then(players => players.sort((a, b) => b.elo - a.elo));"),
                Challenge("Processo batch dentro do Application_Start", "Ao subir a aplicação, um processamento pesado roda no Application_Start e deixa o site indisponível. Explique ciclo de vida e alternativa.", "Difícil", "ASP.NET", 1200, "30 min", 1640,
@"protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    ImportAllLegacyData();
}"),
                Challenge("Entrevista: discordância com tech lead", "Você discorda de uma decisão técnica do tech lead, mas o prazo está apertado. Responda como conduziria a conversa sem parecer defensivo.", "Médio", "Comunicação", 850, "20 min", 1290,
@"Situação:
- tech lead quer duplicar uma rotina para entregar hoje
- você vê risco de manutenção
- produto pressiona por prazo
Pergunta: como você argumenta?"),
                Challenge("Liderança: dev júnior travado", "Um dev júnior está há dois dias travado e tem vergonha de pedir ajuda. Como você conduziria sem tomar o teclado dele nem expor a pessoa?", "Médio", "Liderança", 900, "20 min", 1350,
@"Contexto:
- tarefa importante
- júnior evita daily
- PR parado
- time remoto
Objetivo: destravar aprendizado e entrega."),
                Challenge("Arquitetura: quando não criar microserviço", "O time quer extrair um microserviço para resolver lentidão, mas o gargalo parece consulta e acoplamento interno. Explique como decidir.", "Difícil", "Arquitetura", 1300, "35 min", 1800,
@"Sintomas:
- endpoint lento
- banco sem índices adequados
- deploy monolítico simples
- time com 3 devs
Proposta do time: criar microserviço de pedidos."),
                Challenge("Qualidade: teste que não testa comportamento", "Um teste só verifica se o método não lança exceção. Explique por que isso dá falsa confiança e escreva critérios melhores.", "Fácil", "Testes", 800, "20 min", 1190,
@"[Test]
public void ShouldSaveOrder()
{
    service.Save(new Order());
    Assert.Pass();
}"),
                Challenge("Observabilidade sem correlação", "Logs existem, mas não há correlation id entre requisição, pagamento e e-mail. Proponha rastreabilidade mínima em MVC 5.", "Difícil", "Observabilidade", 1200, "30 min", 1670,
@"Log.Info(""Starting checkout"");
payment.Charge(order);
Log.Info(""Payment done"");
email.Send(order.Email);
Log.Info(""Email sent"");")
            };
        }

        private static Challenge Challenge(string title, string description, string difficulty, string category, int minimumRating, string estimatedTime, int challengeRating, string code)
        {
            return new Challenge
            {
                Title = title,
                Description = description,
                Difficulty = difficulty,
                Category = category,
                MinimumRating = minimumRating,
                EstimatedTime = estimatedTime,
                ChallengeRating = challengeRating,
                FakeCode = code
            };
        }
    }
}
