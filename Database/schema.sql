-- DevRank database schema
-- Compatible with MySQL/MariaDB 10.5+
-- Default charset keeps Portuguese accents working correctly.

SET NAMES utf8mb4;
SET time_zone = '-03:00';

CREATE TABLE IF NOT EXISTS programmers (
    id INT NOT NULL AUTO_INCREMENT,
    username VARCHAR(80) NOT NULL,
    password VARCHAR(120) NOT NULL,
    name VARCHAR(160) NOT NULL,
    fake_photo_url MEDIUMTEXT NULL,
    main_stack VARCHAR(120) NOT NULL,
    secondary_stack VARCHAR(120) NULL,
    perceived_seniority VARCHAR(80) NULL,
    bio TEXT NULL,
    experience_time VARCHAR(80) NULL,
    fake_github VARCHAR(180) NULL,
    fake_linkedin VARCHAR(180) NULL,
    fake_portfolio VARCHAR(180) NULL,
    languages_text VARCHAR(300) NULL,
    elo_rating INT NOT NULL DEFAULT 1000,
    wins INT NOT NULL DEFAULT 0,
    losses INT NOT NULL DEFAULT 0,
    win_streak INT NOT NULL DEFAULT 0,
    community_reputation INT NOT NULL DEFAULT 0,
    community_level INT NOT NULL DEFAULT 1,
    helpful_reviews INT NOT NULL DEFAULT 0,
    season VARCHAR(80) NOT NULL DEFAULT 'Season 01',
    level_name VARCHAR(80) NOT NULL DEFAULT 'Bronze',
    badges_text VARCHAR(500) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    PRIMARY KEY (id),
    UNIQUE KEY ux_programmers_username (username)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS challenges (
    id INT NOT NULL AUTO_INCREMENT,
    title VARCHAR(180) NOT NULL,
    description TEXT NOT NULL,
    difficulty VARCHAR(80) NOT NULL,
    category VARCHAR(100) NOT NULL,
    minimum_rating INT NOT NULL DEFAULT 0,
    estimated_time VARCHAR(80) NULL,
    fake_code TEXT NULL,
    challenge_rating INT NOT NULL DEFAULT 1200,
    is_active BIT NOT NULL DEFAULT b'1',
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS match_histories (
    id INT NOT NULL AUTO_INCREMENT,
    programmer_id INT NOT NULL,
    challenge_id INT NOT NULL,
    challenge_title VARCHAR(180) NOT NULL,
    approved BIT NOT NULL,
    score INT NOT NULL,
    elo_before INT NOT NULL,
    elo_after INT NOT NULL,
    elo_delta INT NOT NULL,
    feedback TEXT NULL,
    date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    KEY ix_match_histories_programmer_id (programmer_id),
    KEY ix_match_histories_challenge_id (challenge_id),
    CONSTRAINT fk_match_histories_programmer
        FOREIGN KEY (programmer_id) REFERENCES programmers(id),
    CONSTRAINT fk_match_histories_challenge
        FOREIGN KEY (challenge_id) REFERENCES challenges(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS community_challenges (
    id INT NOT NULL AUTO_INCREMENT,
    author_id INT NOT NULL,
    author_name VARCHAR(160) NOT NULL,
    title VARCHAR(180) NOT NULL,
    type VARCHAR(100) NOT NULL,
    category VARCHAR(100) NOT NULL,
    scenario TEXT NOT NULL,
    expected_answer TEXT NULL,
    status VARCHAR(80) NOT NULL DEFAULT 'Em homologação',
    homologation_stage VARCHAR(120) NOT NULL DEFAULT 'Community Review',
    required_approvals INT NOT NULL DEFAULT 3,
    approval_votes INT NOT NULL DEFAULT 0,
    rejection_votes INT NOT NULL DEFAULT 0,
    confidence_score INT NOT NULL DEFAULT 0,
    quality_score INT NOT NULL DEFAULT 0,
    clarity_score INT NOT NULL DEFAULT 0,
    relevance_score INT NOT NULL DEFAULT 0,
    difficulty_score INT NOT NULL DEFAULT 0,
    approval_rate INT NOT NULL DEFAULT 0,
    abandon_rate INT NOT NULL DEFAULT 0,
    technical_level INT NOT NULL DEFAULT 1,
    reports INT NOT NULL DEFAULT 0,
    moderator_note TEXT NULL,
    is_official_candidate BIT NOT NULL DEFAULT b'0',
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NULL,
    PRIMARY KEY (id),
    KEY ix_community_challenges_author_id (author_id),
    KEY ix_community_challenges_status (status),
    CONSTRAINT fk_community_challenges_author
        FOREIGN KEY (author_id) REFERENCES programmers(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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

CREATE TABLE IF NOT EXISTS profile_skills (
    id INT NOT NULL AUTO_INCREMENT,
    programmer_id INT NOT NULL,
    name VARCHAR(120) NOT NULL,
    score INT NOT NULL DEFAULT 0,
    skill_group VARCHAR(80) NOT NULL,
    PRIMARY KEY (id),
    KEY ix_profile_skills_programmer_id (programmer_id),
    CONSTRAINT fk_profile_skills_programmer
        FOREIGN KEY (programmer_id) REFERENCES programmers(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS tech_elos (
    id INT NOT NULL AUTO_INCREMENT,
    programmer_id INT NOT NULL,
    technology VARCHAR(120) NOT NULL,
    rating INT NOT NULL,
    rank_name VARCHAR(80) NOT NULL,
    PRIMARY KEY (id),
    KEY ix_tech_elos_programmer_id (programmer_id),
    CONSTRAINT fk_tech_elos_programmer
        FOREIGN KEY (programmer_id) REFERENCES programmers(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

