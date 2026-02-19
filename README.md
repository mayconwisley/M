# M - Utilitário de Linha de Comando (.NET)

Ferramenta CLI desenvolvida em .NET para automação de tarefas
relacionadas a:

-   Controle de tempo (timeout)
-   Mesclagem de PDFs e imagens
-   Operações com diretórios
-   Cópia e movimentação de arquivos

------------------------------------------------------------------------

# Requisitos

-   .NET 8+ (ou versão compatível com o projeto)
-   Pacote NuGet:
    -   PdfSharp

Instalação do pacote:

``` bash
dotnet add package PdfSharp
```

------------------------------------------------------------------------

# Compilação

``` bash
dotnet build -c Release
```

Publicação:

``` bash
dotnet publish -c Release -r win-x64 --self-contained true
```

------------------------------------------------------------------------

# Estrutura de Comandos

    M -timeout <tempo_em_segundos>
    M -merge <diretorio_entrada> <diretorio_saida>
    M -folder <opcao> [parametros]

------------------------------------------------------------------------

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

Validações: - Deve conter exatamente 2 argumentos - O tempo deve ser
número inteiro

------------------------------------------------------------------------

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
M -merge <diretorio_entrada> <diretorio_saida>
```

## Exemplo

``` bash
M -merge C:\Docs\Entrada C:\Docs\Saida
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

------------------------------------------------------------------------

# 3. Operações com Pastas (-folder)

Subcomandos disponíveis:

c → Criar diretório\
m → Mover diretório\
k → Copiar ou recortar arquivos

------------------------------------------------------------------------

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
-   Nome não pode conter caracteres inválidos

------------------------------------------------------------------------

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

------------------------------------------------------------------------

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

------------------------------------------------------------------------

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

------------------------------------------------------------------------

# Códigos de Saída

  Código   Significado
  -------- ----------------------
  0        Execução concluída
  1        Erro interno
  2        Argumentos inválidos

------------------------------------------------------------------------

# Observações Técnicas

-   Operações são síncronas e bloqueantes
-   Não há suporte a subdiretórios
-   Merge gera nome fixo: pdfMerge.pdf

------------------------------------------------------------------------

# Licença

Definir conforme necessidade do projeto (MIT, Proprietária, etc).
