# M - Utilitário de Linha de Comando (.NET)

Ferramenta CLI desenvolvida em .NET para automação de tarefas
relacionadas a:

-   Controle de tempo (timeout)
-   Mesclagem de PDFs e imagens
-   Operações com diretórios
-   Cópia e movimentação de arquivos

---

# Requisitos

-   .NET 8+ (ou versão compatível com o projeto)
-   Pacote NuGet:
    -   PdfSharp

Instalação do pacote:

``` bash
dotnet add package PdfSharp
```

---

# Compilação

``` bash
dotnet build -c Release
```

Publicação:

``` bash
dotnet publish -c Release -r win-x64 --self-contained true
```

---

# Estrutura de Comandos

    M -timeout <tempo_em_segundos>
    M -merge <diretorio_entrada> <diretorio_saida> [nome_arquivo_saida]
    M -folder <opcao> [parametros]
    M -file <opcao> [parametros]

---

# Exemplos Rápidos

``` bash
M -timeout 5
M -merge C:\Docs\Entrada C:\Docs\Saida relatorio_final
M -folder c C:\Temp NovaPasta
M -folder ds C:\Temp\NovaPasta
M -folder d C:\Temp\NovaPasta
M -file k C:\Entrada\foto.png C:\Backup
M -file d C:\Temp\relatorio.txt
```

---

# 1. Timeout

Executa uma pausa bloqueante utilizando:

``` csharp
Thread.Sleep(timeout * 1000);
```

## Sintaxe

``` bash
M -timeout <tempo>
```

## Exemplo

``` bash
M -timeout 10
```

Aguarda 10 segundos antes de finalizar o processo.

Validações:

-   Deve conter exatamente 2 argumentos
-   O tempo deve ser número inteiro

---

# 2. Merge de PDFs e Imagens

Mescla arquivos localizados em um diretório em um único PDF.

Extensões suportadas:

-   .pdf
-   .jpg
-   .jpeg
-   .png
-   .bmp

Arquivos são processados em ordem alfabética.

## Sintaxe

``` bash
M -merge <diretorio_entrada> <diretorio_saida> [nome_arquivo_saida]
```

## Exemplo

``` bash
M -merge C:\Docs\Entrada C:\Docs\Saida
M -merge C:\Docs\Entrada C:\Docs\Saida relatorio_final
```

Resultado:

    C:\Docs\Saida\pdfMerge.pdf

Comportamento técnico:

-   PDFs são importados via PdfReader.Open(..., Import)
-   Imagens são convertidas em páginas PDF
-   Operação ocorre apenas no diretório raiz (sem subpastas)

Erros possíveis:

-   DirectoryNotFoundException
-   InvalidOperationException

---

## 3. Operações com Pastas (-folder)

Subcomandos disponíveis:

-   `c` → Criar diretório
-   `m` → Mover diretório
-   `d` → Deletar diretório
-   `k` → Copiar ou recortar arquivos
-   `ds` → Deletar subpastas

---

## 3.1 Criar Pasta

``` bash
M -folder c <path> <nome_da_pasta>
```

Exemplo:

``` bash
M -folder c C:\Temp NovaPasta
```

Regras:

-   Caminho deve existir
-   Nome não pode ser vazio ou conter apenas espaços
-   Nome não pode conter caracteres inválidos

---

## 3.2 Mover Pasta

``` bash
M -folder m <origem> <destino>
```

Exemplo:

``` bash
M -folder m C:\Temp\NovaPasta C:\Destino
```

Regras:

-   Origem e destino devem existir
-   Destino final não pode existir previamente

---

## 3.3 Copiar Arquivos

``` bash
M -folder k <origem> <destino>
```

Exemplo:

``` bash
M -folder k C:\Entrada C:\Backup
```

-   Copia todos arquivos do diretório origem
-   Sobrescreve se já existir
-   Não copia subpastas

---

## 3.4 Recortar (Mover Arquivos)

``` bash
M -folder k x <origem> <destino>
```

Exemplo:

``` bash
M -folder k x C:\Entrada C:\Destino
```

-   Move arquivos usando File.Move
-   Remove da origem

---

## 3.5 Deletar Pasta

``` bash
M -folder d <path>
```

Exemplo:

``` bash
M -folder d C:\Temp\NovaPasta
```

Regras:

-   O diretório deve existir
-   A exclusão é recursiva (remove subpastas e arquivos)
---

## 3.6 Deletar Subpastas

Remove todas as subpastas de um diretório, mantendo os arquivos na raiz.

``` bash
M -folder ds <path>
```

Exemplo:

``` bash
M -folder ds C:\Temp\NovaPasta
```

Regras:

-   O diretório deve existir
-   Remove apenas subpastas (recursivo)
-   Arquivos na raiz são preservados
---

## 4. Operações com Arquivos (-file)

Subcomandos disponíveis:

-   `c` → Criar arquivo
-   `m` → Mover arquivo
-   `d` → Deletar arquivo
-   `k` → Copiar ou recortar arquivo

---

## 4.1 Criar Arquivo

``` bash
M -file c <diretorio> <nome_arquivo>
```

Exemplo:

``` bash
M -file c C:\Temp relatorio.txt
```

Regras:

-   Diretório deve existir
-   Nome do arquivo não pode ser vazio ou conter apenas espaços
-   Nome do arquivo não pode conter caracteres inválidos

---

## 4.2 Mover Arquivo

``` bash
M -file m <arquivo_origem> <diretorio_destino>
```

Exemplo:

``` bash
M -file m C:\Temp\relatorio.txt C:\Destino
```

Regras:

-   Arquivo de origem e diretório de destino devem existir
-   Não substitui arquivo existente no destino

---

## 4.3 Copiar Arquivo

``` bash
M -file k <arquivo_origem> <diretorio_destino>
```

Exemplo:

``` bash
M -file k C:\Entrada\foto.png C:\Backup
```

-   Copia o arquivo para o diretório de destino
-   Sobrescreve se já existir

---

## 4.4 Recortar Arquivo

``` bash
M -file k x <arquivo_origem> <diretorio_destino>
```

Exemplo:

``` bash
M -file k x C:\Entrada\foto.png C:\Destino
```

-   Move o arquivo para o destino
-   Sobrescreve se já existir

---

## 4.5 Deletar Arquivo

``` bash
M -file d <arquivo>
```

Exemplo:

``` bash
M -file d C:\Temp\relatorio.txt
```

Regras:

-   O arquivo deve existir

---

# Códigos de Saída

| Código | Significado |
| --- | --- |
| 0 | Execução concluída |
| 1 | Erro interno |
| 2 | Argumentos inválidos |

---

# Observações Técnicas

-   Operações são síncronas e bloqueantes
-   Merge ocorre apenas no diretório raiz (sem subpastas)
-   Cópia de arquivos via `-folder k` não inclui subpastas
-   Merge usa nome padrão pdfMerge.pdf quando nome de saída não é informado

---

# Testes

Projeto de testes automatizados:

-   `M.Tests` (xUnit)

Execução local:

``` bash
dotnet restore M.slnx --configfile NuGet.Config
dotnet test M.Tests\M.Tests.csproj -c Release
```

---

# CI/CD (GitHub Actions)

Workflow:

-   `.github\workflows\ci.yml`

Gatilhos:

-   Push em qualquer branch
-   Pull Request

Pipeline executa:

1. `dotnet restore`
2. `dotnet build` (Release)
3. `dotnet test` (Release)

---

# Licença

Definir conforme necessidade do projeto (MIT, Proprietária, etc).
