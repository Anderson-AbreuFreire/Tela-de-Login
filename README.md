# Sistema de Chamados - PIM 3 Semestre ADS

Este projeto é um sistema básico de gerenciamento de chamados com autenticação de usuários, utilizando C# com PostgreSQL.
As senhas são armazenadas como texto puro apenas para fins didáticos se for usar troque para um sistema de segurança usando hash
---

## ✅ Requisitos

Antes de executar o projeto, certifique-se de ter:

- PostgreSQL instalado e em execução localmente
- Visual Studio (ou outro ambiente de desenvolvimento C#)
- Pacote `Npgsql` instalado (via NuGet)

---

## ⚙️ Configuração do Banco de Dados

1. Crie um banco de dados chamado `pim` no PostgreSQL.
2. Execute os scripts SQL disponíveis na pasta `scripts/` do repositório.
   - Eles criam as tabelas `departamento`, `funcionario` e `chamado`.
   - Também inserem os departamentos iniciais: **RH**, **Produção** e **Gerência**.

---

## 🔌 String de Conexão

A string de conexão está localizada em `Conexao.cs`:
Reajuste de acordo com as credênciais de seu banco de dados

```csharp
public static class Conexao
{
    public static string ConexaoString { get; } =
        "Host=localhost;Port=5432;Database=pim;User ID=postgres;Password=belofode";
}
