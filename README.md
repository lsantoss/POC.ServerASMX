# ServerASMX

<h3>Aplicação:</h3>
<p>Essa aplicação contém um exemplo básico de um crud em um Servidor ASMX</p>
<p>Banco de Dados: Não</p>

<h3>Como fazer:</h3>
<ol type="number">
  <li>Criar um projeto <b>Web Vazio</b> (todo vazio)</li>
  <li>Adicione as classes</li>
  <li>Adicione um novo arquivo do tipo <b>Serviço Web (ASMX)</b></li>  
  <li>Nos métodos use a assinatura: <b>[WebMethod]</b></li>   
  <li>
    Exemplo de método:
    <blockquote>
      [WebMethod]<br/>
        public List<Pessoa> TodasAsPessoas()<br/>
        {<br/>
            return Pessoa.Pessoas;<br/>
        }<br/>
      </blockquote>
  </li>
</ol>
