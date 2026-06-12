# M — File Toolkit

CLI em .NET para automação de operações com arquivos, pastas e PDFs.

---

## Requisitos

- .NET 10
- Pacote NuGet: [PDFsharp](https://www.nuget.org/packages/PDFsharp)

---

## Compilação e publicação

```bash
# Build
dotnet build -c Release

# Publicar executável self-contained para Windows x64
dotnet publish -c Release -r win-x64 --self-contained true
```

---

## Estrutura do projeto

```
M/
├── Domain/            # Interfaces — sem dependências externas
├── Application/       # Use cases — regras de negócio e validação
├── Infrastructure/    # Implementações de I/O (arquivo, PDF)
├── Presentation/      # CLI handlers e ConsoleUi
└── Program.cs         # Composition root
```

---

## Comandos

```
M -timeout <seconds>
M -merge   <input_dir> <output_dir> [filename]
M -folder  <cmd> [args...]
M -file    <cmd> [args...]
M --help
```

---

## -timeout

Pausa bloqueante por N segundos.

```bash
M -timeout 10
```

---

## -merge

Mescla PDFs e imagens em um único arquivo PDF, processados em ordem alfabética.

**Extensões suportadas:** `.pdf` `.jpg` `.jpeg` `.png` `.bmp`

```bash
M -merge C:\Docs\Entrada C:\Docs\Saida
M -merge C:\Docs\Entrada C:\Docs\Saida relatorio_final
```

Quando o nome de saída é omitido, o arquivo gerado se chama `pdfMerge.pdf`.

---

## -folder

Operações sobre diretórios.

| Subcomando | Sintaxe | Descrição |
|---|---|---|
| `c` | `-folder c <path> <name>` | Criar subpasta |
| `m` | `-folder m <src> <dst>` | Mover pasta |
| `d` | `-folder d <path>` | Deletar pasta (recursivo) |
| `ds` | `-folder ds <path>` | Deletar subpastas |
| `da` | `-folder da <path>` | Deletar arquivos da pasta |
| `k` | `-folder k <src> <dst>` | Copiar arquivos da pasta |
| `k x` | `-folder k x <src> <dst>` | Recortar arquivos da pasta |

```bash
M -folder c  C:\Temp NovaPasta
M -folder m  C:\Temp\NovaPasta C:\Destino
M -folder d  C:\Temp\NovaPasta
M -folder ds C:\Temp\NovaPasta
M -folder da C:\Temp\NovaPasta
M -folder k  C:\Entrada C:\Backup
M -folder k x C:\Entrada C:\Destino
```

---

## -file

Operações sobre arquivos individuais.

| Subcomando | Sintaxe | Descrição |
|---|---|---|
| `c` | `-file c <dir> <name>` | Criar arquivo |
| `m` | `-file m <file> <dst>` | Mover arquivo |
| `d` | `-file d <file>` | Deletar arquivo |
| `k` | `-file k <file> <dst>` | Copiar arquivo |
| `k x` | `-file k x <file> <dst>` | Recortar arquivo |

```bash
M -file c   C:\Temp relatorio.txt
M -file m   C:\Temp\relatorio.txt C:\Destino
M -file d   C:\Temp\relatorio.txt
M -file k   C:\Entrada\foto.png C:\Backup
M -file k x C:\Entrada\foto.png C:\Destino
```

---

## Códigos de saída

| Código | Significado |
|---|---|
| `0` | Sucesso |
| `1` | Erro de execução |
| `2` | Argumentos inválidos |

---

## Testes

```bash
dotnet test
```

Projeto: `M.Tests` (xUnit). Os testes cobrem os repositórios de infraestrutura com diretórios temporários reais.

---

## CI

Pipeline em `.github/workflows/ci.yml` — executa em todo push e pull request:

1. Restore
2. Build (Release)
3. Test (Release) + publicação de resultados como artefato
