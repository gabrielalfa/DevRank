-- DevRank seed data
-- Run after Database/schema.sql.

SET NAMES utf8mb4;

INSERT INTO programmers
    (id, username, password, name, fake_photo_url, main_stack, secondary_stack, perceived_seniority, bio, experience_time, fake_github, fake_linkedin, fake_portfolio, languages_text, elo_rating, wins, losses, win_streak, community_reputation, community_level, helpful_reviews, season, level_name, badges_text)
VALUES
    (1, 'ana', '123', 'Ana Byte', 'avatar://ana-byte', 'ASP.NET MVC', 'Arquitetura / SQL', 'Senior', 'Especialista em refatoração de sistemas legados.', '8 anos', 'github.com/anabyte', 'linkedin.com/in/anabyte', 'anabyte.dev', 'Português, Inglês técnico', 2310, 48, 12, 7, 1840, 4, 88, 'Season 01', 'Diamante', 'Legacy Slayer, MVC Elite, Clean Fix'),
    (2, 'bruno', '123', 'Bruno Stack', 'avatar://bruno-stack', 'C# / SQL', 'Performance', 'Pleno+', 'Resolve gargalos antes do café esfriar.', '6 anos', 'github.com/brunostack', 'linkedin.com/in/brunostack', 'brunostack.dev', 'Português', 1975, 34, 15, 4, 1320, 3, 52, 'Season 01', 'Platina', 'Perf Hunter, Query Tamer'),
    (3, 'carla', '123', 'Carla Thread', 'avatar://carla-thread', 'Backend', 'Concorrência / Incidentes', 'Senior', 'Boa em concorrência, filas e bugs intermitentes.', '7 anos', 'github.com/carlathread', 'linkedin.com/in/carlathread', 'carlathread.io', 'Português, Inglês', 1740, 28, 14, 2, 980, 3, 37, 'Season 01', 'Ouro', 'Race Detector, Service Mind'),
    (4, 'diego', '123', 'Diego Razor', 'avatar://diego-razor', 'Razor / JavaScript', 'UX / Frontend', 'Pleno', 'Transforma tela quebrada em fluxo usável.', '4 anos', 'github.com/diegorazor', 'linkedin.com/in/diegorazor', 'diegorazor.dev', 'Português, Espanhol', 1510, 19, 11, 3, 520, 2, 18, 'Season 01', 'Prata', 'UI Fixer, Vanilla JS'),
    (5, 'eva', '123', 'Eva Commit', 'avatar://eva-commit', 'Full Stack', 'Produto / Comunicação', 'Junior+', 'Entrega soluções simples, testáveis e diretas.', '2 anos', 'github.com/evacommit', 'linkedin.com/in/evacommit', 'evacommit.dev', 'Português', 1320, 14, 13, 1, 210, 1, 9, 'Season 01', 'Prata', 'Fast Ship, Bug Basher')
ON DUPLICATE KEY UPDATE
    name = VALUES(name),
    elo_rating = VALUES(elo_rating),
    community_reputation = VALUES(community_reputation);

INSERT INTO challenges
    (id, title, description, difficulty, category, minimum_rating, estimated_time, challenge_rating, fake_code)
VALUES
    (1, 'Refatorar endpoint MVC', 'Um endpoint mistura regra de negócio, validação e acesso a dados. Proponha uma refatoração simples para melhorar manutenção.', 'Médio', 'ASP.NET MVC', 900, '20 min', 1350, 'public ActionResult Save(Order order)\r\n{\r\n    if (order.Total <= 0) return View(order);\r\n    var db = new AppDbContext();\r\n    db.Orders.Add(order);\r\n    db.SaveChanges();\r\n    SendEmail(order.CustomerEmail);\r\n    return RedirectToAction("Index");\r\n}'),
    (2, 'Corrigir query lenta', 'Uma listagem está lenta em horário de pico. Explique como reduzir consultas repetidas e melhorar a leitura dos dados.', 'Médio', 'Performance', 1000, '25 min', 1450, 'foreach (var customer in customers)\r\n{\r\n    customer.LastOrder = db.Orders\r\n        .Where(o => o.CustomerId == customer.Id)\r\n        .OrderByDescending(o => o.CreatedAt)\r\n        .FirstOrDefault();\r\n}'),
    (3, 'Detectar bug de concorrência', 'Dois usuários podem consumir o mesmo cupom ao mesmo tempo. Identifique o risco e proponha uma correção pragmática.', 'Difícil', 'Concorrência', 1300, '30 min', 1750, 'var coupon = repository.Get(code);\r\nif (!coupon.Used)\r\n{\r\n    coupon.Used = true;\r\n    repository.Save(coupon);\r\n    ApplyDiscount(coupon);\r\n}'),
    (4, 'Melhorar arquitetura de service', 'Um service concentra validação, persistência, notificação e logs. Quebre responsabilidades sem criar arquitetura exagerada.', 'Médio', 'Arquitetura', 1100, '25 min', 1500, 'public class BillingService\r\n{\r\n    public void Charge(Invoice invoice)\r\n    {\r\n        Validate(invoice);\r\n        Save(invoice);\r\n        SendEmail(invoice);\r\n        WriteAudit(invoice);\r\n    }\r\n}')
ON DUPLICATE KEY UPDATE
    title = VALUES(title),
    description = VALUES(description);

INSERT INTO community_challenges
    (id, author_id, author_name, title, challenge_type, category, scenario, expected_answer, status, homologation_stage, required_approvals, approval_votes, rejection_votes, confidence_score, quality_score, clarity_score, relevance_score, difficulty_score, approval_rate, abandon_rate, technical_level, reports, moderator_note, is_official_candidate)
VALUES
    (1, 1, 'Ana Byte', 'Incidente de API com timeout intermitente', 'Cenário real', 'Debugging', 'Uma API MVC começa a responder acima de 8s durante pico. O time precisa mitigar, comunicar e investigar sem derrubar produção.', 'Diagnóstico incremental, logs, métricas, rollback controlado e comunicação objetiva.', 'Homologado', 'Homologado pela comunidade', 3, 5, 0, 94, 94, 91, 96, 82, 88, 11, 4, 0, 'Excelente cenário de produção.', 1),
    (2, 2, 'Bruno Stack', 'Query N+1 em relatório financeiro', 'Desafio técnico', 'SQL', 'Relatório executa centenas de consultas por cliente e trava no fechamento mensal.', 'Identificar N+1, projetar consulta agregada, paginar e medir antes/depois.', 'Em homologação', 'Community Review', 3, 2, 0, 76, 86, 78, 92, 74, 0, 0, 3, 0, 'Precisa validar critérios de aceite.', 1)
ON DUPLICATE KEY UPDATE
    status = VALUES(status),
    homologation_stage = VALUES(homologation_stage),
    confidence_score = VALUES(confidence_score);
