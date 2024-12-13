--Exemplo de como deve ser a atualizacao dos pontos para varios usuarios de uma vez
UPDATE tbl_user SET points = points + 4 WHERE Id in (1,2,3,4,5)