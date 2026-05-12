CREATE TABLE IF NOT EXISTS community_reviews (
    id INT NOT NULL AUTO_INCREMENT,
    challenge_id INT NOT NULL,
    reviewer_name VARCHAR(160) NOT NULL,
    decision VARCHAR(80) NOT NULL,
    comment TEXT NULL,
    clarity_score INT NOT NULL DEFAULT 0,
    relevance_score INT NOT NULL DEFAULT 0,
    reviewer_reputation INT NOT NULL DEFAULT 0,
    reputation_delta INT NOT NULL DEFAULT 0,
    date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    KEY ix_community_reviews_challenge_id (challenge_id),
    CONSTRAINT fk_community_reviews_challenge
        FOREIGN KEY (challenge_id) REFERENCES community_challenges(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO community_reviews
    (challenge_id, reviewer_name, decision, comment, clarity_score, relevance_score, reviewer_reputation, reputation_delta, date)
SELECT 4, 'Ana Byte', 'Devolvido', 'Cenário bom, mas falta código inicial e métrica de sucesso.', 58, 75, 1840, 12, DATE_SUB(NOW(), INTERVAL 2 DAY)
WHERE EXISTS (SELECT 1 FROM community_challenges WHERE id = 4)
  AND NOT EXISTS (SELECT 1 FROM community_reviews WHERE challenge_id = 4 AND reviewer_name = 'Ana Byte');

INSERT INTO community_reviews
    (challenge_id, reviewer_name, decision, comment, clarity_score, relevance_score, reviewer_reputation, reputation_delta, date)
SELECT 5, 'Bruno Stack', 'Rejeitado', 'Muito abstrato. Precisa de conflito real e resposta esperada mais objetiva.', 46, 55, 1320, 8, DATE_SUB(NOW(), INTERVAL 3 DAY)
WHERE EXISTS (SELECT 1 FROM community_challenges WHERE id = 5)
  AND NOT EXISTS (SELECT 1 FROM community_reviews WHERE challenge_id = 5 AND reviewer_name = 'Bruno Stack');

INSERT INTO community_reviews
    (challenge_id, reviewer_name, decision, comment, clarity_score, relevance_score, reviewer_reputation, reputation_delta, date)
SELECT 2, 'Carla Thread', 'Em análise', 'Bom desafio, aguardando critérios de aceite.', 78, 92, 980, 6, DATE_SUB(NOW(), INTERVAL 12 HOUR)
WHERE EXISTS (SELECT 1 FROM community_challenges WHERE id = 2)
  AND NOT EXISTS (SELECT 1 FROM community_reviews WHERE challenge_id = 2 AND reviewer_name = 'Carla Thread');
