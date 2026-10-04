param(
    [string]$BaseUrl = "http://localhost:5099",
    [string]$OutputPath = "$PSScriptRoot/resultados-testes.json"
)

$results = [System.Collections.Generic.List[object]]::new()
$sequence = 0
$suffix = [DateTimeOffset]::UtcNow.ToUnixTimeSeconds().ToString().Substring(4)

function Send-Request {
    param([string]$Method, [string]$Path, $Body = $null, [string]$Name = "", [bool]$Record = $true)
    $headers = @{}
    $parameters = @{ Uri = "$BaseUrl$Path"; Method = $Method; UseBasicParsing = $true; Headers = $headers }
    $sentBody = $null
    if ($null -ne $Body) {
        $sentBody = $Body | ConvertTo-Json -Depth 8 -Compress
        $parameters.ContentType = "application/json"
        $parameters.Body = $sentBody
    }
    try {
        $response = Invoke-WebRequest @parameters
        $status = [int]$response.StatusCode
        $responseBody = $response.Content
    }
    catch {
        $status = [int]$_.Exception.Response.StatusCode
        $stream = $_.Exception.Response.GetResponseStream()
        $reader = [System.IO.StreamReader]::new($stream)
        $responseBody = $reader.ReadToEnd()
        $reader.Dispose()
    }
    if ($Record) {
        $script:sequence++
        $script:results.Add([pscustomobject]@{
            Numero = $script:sequence
            Nome = $Name
            Metodo = $Method
            Endpoint = $Path
            CorpoEnviado = $sentBody
            Status = $status
            Retorno = $responseBody
            ExecutadoEm = (Get-Date).ToString("yyyy-MM-dd HH:mm:ss")
        })
    }
    return [pscustomobject]@{ Status = $status; Body = $responseBody }
}

function Get-Id($response) {
    if ($response.Status -ne 201) {
        throw "Falha ao criar dado de apoio. Status: $($response.Status). Retorno: $($response.Body)"
    }
    return [int](($response.Body | ConvertFrom-Json).id)
}

$fabricante = Send-Request POST "/api/fabricantes" @{ nome = "Teste Fabricante $suffix" } "Cadastrar fabricante"
$fabricanteId = Get-Id $fabricante
Send-Request GET "/api/fabricantes" $null "Listar fabricantes" | Out-Null
Send-Request GET "/api/fabricantes/$fabricanteId" $null "Consultar fabricante por ID" | Out-Null
Send-Request PUT "/api/fabricantes/$fabricanteId" @{ nome = "Teste Fabricante Atualizado $suffix" } "Atualizar fabricante" | Out-Null
Send-Request DELETE "/api/fabricantes/$fabricanteId" $null "Excluir fabricante" | Out-Null

$categoria = Send-Request POST "/api/categoriasveiculos" @{ nome = "Teste Categoria $suffix"; descricao = "Categoria criada pelos testes da Etapa 3" } "Cadastrar categoria"
$categoriaId = Get-Id $categoria
Send-Request GET "/api/categoriasveiculos" $null "Listar categorias" | Out-Null
Send-Request GET "/api/categoriasveiculos/$categoriaId" $null "Consultar categoria por ID" | Out-Null
Send-Request PUT "/api/categoriasveiculos/$categoriaId" @{ nome = "Teste Categoria Atualizada $suffix"; descricao = "Categoria atualizada" } "Atualizar categoria" | Out-Null
Send-Request DELETE "/api/categoriasveiculos/$categoriaId" $null "Excluir categoria" | Out-Null

$cliente = Send-Request POST "/api/clientes" @{ nome = "Cliente Teste"; cpf = "7$suffix".PadRight(11, '0').Substring(0, 11); email = "cliente.$suffix@exemplo.com"; telefone = "31999990000" } "Cadastrar cliente"
$clienteId = Get-Id $cliente
Send-Request GET "/api/clientes" $null "Listar clientes" | Out-Null
Send-Request GET "/api/clientes/$clienteId" $null "Consultar cliente por ID" | Out-Null
Send-Request PUT "/api/clientes/$clienteId" @{ nome = "Cliente Teste Atualizado"; cpf = "7$suffix".PadRight(11, '0').Substring(0, 11); email = "cliente.$suffix@exemplo.com"; telefone = "31999991111" } "Atualizar cliente" | Out-Null
Send-Request DELETE "/api/clientes/$clienteId" $null "Excluir cliente" | Out-Null

$fabricanteApoio = Send-Request POST "/api/fabricantes" @{ nome = "Fiat Teste $suffix" } "" $false
$fabricanteApoioId = Get-Id $fabricanteApoio
$categoriaApoio = Send-Request POST "/api/categoriasveiculos" @{ nome = "SUV Teste $suffix"; descricao = "SUV ficticio" } "" $false
$categoriaApoioId = Get-Id $categoriaApoio

$veiculo = Send-Request POST "/api/veiculos" @{ modelo = "Pulse Teste"; placa = "T$suffix".PadRight(7, '0').Substring(0, 7); ano = 2025; quilometragem = 100; valorDiaria = 180.50; fabricanteId = $fabricanteApoioId; categoriaVeiculoId = $categoriaApoioId } "Cadastrar veículo"
$veiculoId = Get-Id $veiculo
Send-Request GET "/api/veiculos" $null "Listar veículos" | Out-Null
Send-Request GET "/api/veiculos/$veiculoId" $null "Consultar veículo por ID" | Out-Null
Send-Request PUT "/api/veiculos/$veiculoId" @{ modelo = "Pulse Teste Atualizado"; placa = "T$suffix".PadRight(7, '0').Substring(0, 7); ano = 2025; quilometragem = 250; valorDiaria = 190.00; fabricanteId = $fabricanteApoioId; categoriaVeiculoId = $categoriaApoioId } "Atualizar veículo" | Out-Null
Send-Request DELETE "/api/veiculos/$veiculoId" $null "Excluir veículo" | Out-Null

$clienteApoio = Send-Request POST "/api/clientes" @{ nome = "Cliente Aluguel Teste"; cpf = "8$suffix".PadRight(11, '0').Substring(0, 11); email = "aluguel.$suffix@exemplo.com"; telefone = "31988880000" } "" $false
$clienteApoioId = Get-Id $clienteApoio
$clienteSemAluguel = Send-Request POST "/api/clientes" @{ nome = "Cliente Sem Aluguel"; cpf = "9$suffix".PadRight(11, '0').Substring(0, 11); email = "semaluguel.$suffix@exemplo.com"; telefone = "31977770000" } "" $false
$clienteSemAluguelId = Get-Id $clienteSemAluguel
$placaAluguel = "A$suffix".PadRight(7, '0').Substring(0, 7)
$veiculoApoio = Send-Request POST "/api/veiculos" @{ modelo = "Argo Teste"; placa = $placaAluguel; ano = 2024; quilometragem = 1000; valorDiaria = 150.00; fabricanteId = $fabricanteApoioId; categoriaVeiculoId = $categoriaApoioId } "" $false
$veiculoApoioId = Get-Id $veiculoApoio
$veiculoSemHistorico = Send-Request POST "/api/veiculos" @{ modelo = "Toro Sem Historico"; placa = "H$suffix".PadRight(7, '0').Substring(0, 7); ano = 2023; quilometragem = 2000; valorDiaria = 220.00; fabricanteId = $fabricanteApoioId; categoriaVeiculoId = $categoriaApoioId } "" $false
$veiculoSemHistoricoId = Get-Id $veiculoSemHistorico

$aluguelBody = @{ clienteId = $clienteApoioId; veiculoId = $veiculoApoioId; dataInicio = "2026-10-04T10:00:00"; dataFimPrevista = "2026-10-07T10:00:00"; dataDevolucao = $null; quilometragemInicial = 1000; quilometragemFinal = $null; valorDiaria = 150.00; valorTotal = $null }
$aluguel = Send-Request POST "/api/alugueis" $aluguelBody "Cadastrar aluguel"
$aluguelId = Get-Id $aluguel
Send-Request GET "/api/alugueis" $null "Listar aluguéis" | Out-Null
Send-Request GET "/api/alugueis/$aluguelId" $null "Consultar aluguel por ID" | Out-Null
$aluguelAtualizado = @{ clienteId = $clienteApoioId; veiculoId = $veiculoApoioId; dataInicio = "2026-10-04T10:00:00"; dataFimPrevista = "2026-10-07T10:00:00"; dataDevolucao = "2026-10-06T15:00:00"; quilometragemInicial = 1000; quilometragemFinal = 1280; valorDiaria = 150.00; valorTotal = 450.00 }
Send-Request PUT "/api/alugueis/$aluguelId" $aluguelAtualizado "Atualizar aluguel" | Out-Null

$fabricanteQuery = [uri]::EscapeDataString("Fiat Teste $suffix")
$categoriaQuery = [uri]::EscapeDataString("SUV Teste $suffix")
$cpfAluguel = "8$suffix".PadRight(11, '0').Substring(0, 11)
Send-Request GET "/api/filtros/veiculos-por-fabricante?fabricante=$fabricanteQuery" $null "Filtrar veículos por fabricante" | Out-Null
Send-Request GET "/api/filtros/veiculos-por-categoria?categoria=$categoriaQuery" $null "Filtrar veículos por categoria" | Out-Null
Send-Request GET "/api/filtros/alugueis-por-cliente?cpf=$cpfAluguel" $null "Filtrar aluguéis por cliente" | Out-Null
Send-Request GET "/api/filtros/clientes-com-alugueis" $null "Listar clientes com ou sem aluguéis" | Out-Null
Send-Request GET "/api/filtros/veiculos-com-historico" $null "Listar veículos com ou sem histórico" | Out-Null

Send-Request DELETE "/api/alugueis/$aluguelId" $null "Excluir aluguel" | Out-Null

Send-Request GET "/api/clientes/99999999" $null "Consultar ID inexistente" | Out-Null
Send-Request POST "/api/fabricantes" @{ nome = " " } "Validar campo obrigatório" | Out-Null
Send-Request POST "/api/veiculos" @{ modelo = "Duplicado"; placa = $placaAluguel; ano = 2024; quilometragem = 0; valorDiaria = 100; fabricanteId = $fabricanteApoioId; categoriaVeiculoId = $categoriaApoioId } "Validar placa duplicada" | Out-Null
Send-Request POST "/api/clientes" @{ nome = "Duplicado"; cpf = $cpfAluguel; email = "outro.$suffix@exemplo.com"; telefone = "31900000000" } "Validar CPF duplicado" | Out-Null
Send-Request POST "/api/veiculos" @{ modelo = "FK Invalida"; placa = "F$suffix".PadRight(7, '0').Substring(0, 7); ano = 2024; quilometragem = 0; valorDiaria = 100; fabricanteId = 99999999; categoriaVeiculoId = $categoriaApoioId } "Validar chave estrangeira" | Out-Null

Send-Request DELETE "/api/veiculos/$veiculoApoioId" $null "" $false | Out-Null
Send-Request DELETE "/api/veiculos/$veiculoSemHistoricoId" $null "" $false | Out-Null
Send-Request DELETE "/api/clientes/$clienteApoioId" $null "" $false | Out-Null
Send-Request DELETE "/api/clientes/$clienteSemAluguelId" $null "" $false | Out-Null
Send-Request DELETE "/api/fabricantes/$fabricanteApoioId" $null "" $false | Out-Null
Send-Request DELETE "/api/categoriasveiculos/$categoriaApoioId" $null "" $false | Out-Null

$results | ConvertTo-Json -Depth 8 | Set-Content -Path $OutputPath -Encoding UTF8
$expectedStatuses = @(201, 200, 200, 204, 204, 201, 200, 200, 204, 204, 201, 200, 200, 204, 204, 201, 200, 200, 204, 204, 201, 200, 200, 204, 200, 200, 200, 200, 200, 204, 404, 400, 409, 409, 400)
$failed = for ($index = 0; $index -lt $results.Count; $index++) {
    if ($results[$index].Status -ne $expectedStatuses[$index]) { $results[$index] }
}
"TESTES=$($results.Count) FALHAS_INESPERADAS=$($failed.Count) SAIDA=$OutputPath"
