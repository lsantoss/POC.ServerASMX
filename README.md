# ServerASMX

<h3>Aplicação:</h3>
<p>Essa aplicação contém um exemplo de Servidor ASMX</p>
<p>Banco de Dados: SQL Server</p>
<p>Biblioteca: Dapper</p>
<p>Biblioteca: Newtonsoft.Json</p>
<p>Biblioteca: NUnit</p>
<p>Biblioteca: NUnit3TestAdapter</p>

<h3>Como fazer:</h3>
<ol type="number">
  <li>Criar um projeto <b>Web Vazio</b> (.net framework)</li>
  <li>Adicione um novo arquivo do tipo <b>Serviço Web (ASMX)</b></li>  
  <li>Nos métodos use a assinatura: <b>[WebMethod]</b></li>   
  <li>
    Exemplo de método:
    <blockquote>
      [WebMethod]<br/>
        public List<Customer> List()<br/>
        {<br/>
            return new List<Customer>();<br/>
        }<br/>
      </blockquote>
  </li>
</ol>

<h3>Observações:</h3>
<p>Não é possíevel realizar injeção de depêndencias</p>
<p>Não é possíevel utilizar interfaces como tipo de retorno</p>
<p>Classes de retorno devem conter um contrutor vazio (para serealização)</p>
