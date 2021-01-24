
import React from 'react';
import AppLoggedIn from './appLoggedIn';
import InputFiled from './InputFile';




class LoginForm extends React.Component{
  
  
  
  constructor(props){

    super(props);
    this.state={
      emailAddress: '',
      password: '',
      password1: '',
      buttonDisabled: false,
      token: undefined,
      isLoggedIn: false,
      dat: '',
      udanelogowanie: true,
      rejsetracja: false,
      userIds: '',



    }
  }
  
 

  srtInputValue(property, val){
    val = val.trim();
    
    this.setState({
      [property]:val
    })
  }


  resetForm(){
    this.setState({
      usernemailAddressame: '',
      password: '',
      password1: '',
      buttonDisabled: false

    })
  }
  
 
  async doLogout(){
    this.setState({udanelogowanie:false});  
    this.setState({userIds:''});  
    this.setState({isLoggedIn:false})  ;
    this.setState({token:''})  ;
    this.setState({rejsetracja:false})  ;
    this.resetForm();

  }

  

  async Registration(){
    this.setState({rejsetracja:true}); 
  }

  async doRegistration(){
    if(!this.state.emailAddress){
      return;
    }

    if(!this.state.password){
      return;
    }
    this.setState({
      buttonDisabled: true
    })
    if(this.state.password!==this.state.password1) alert("Hasła nie są takie same");
    if(this.state.password===this.state.password1)
    try{
      await fetch ('http://localhost:8080/api/users/Create',{
        method: 'POST',
        headers:{
          'Accept': 'application/json-patch+json',
          'Content-Type': 'application/json-patch+json'
        },
        body: JSON.stringify({emailAddress: this.state.emailAddress, password: this.state.password})
      }).then(dat => dat.json()).then(res => this.setState({isLoggedIn: false, token:res.token}));
      

      if(this.state.token==='') {this.setState({udanelogowanie:false}) 
      this.resetForm()} 
      else{alert("Konto zostało założone"); console.log(this.state.token);this.resetForm()
      this.setState({rejsetracja:false})
    }
      
      
    }
    
    
    catch(e){
      console.log(e);
      this.resetForm(e);
      alert("Coś poszło nie tak, spróbuj ponownie");
    }


  }

  async  doLogin(){

      if(!this.state.emailAddress){
        return;
      }

      if(!this.state.password){
        return;
      }
      this.setState({
        buttonDisabled: true
      })

      try{
       await fetch ('http://localhost:8080/api/users/Login',{
          method: 'POST',
          headers:{
            'Accept': 'application/json-patch+json',
            'Content-Type': 'application/json-patch+json'
          },
          body: JSON.stringify({emailAddress: this.state.emailAddress, password: this.state.password})
        }).then(dat => dat.json()).then(res => this.setState({isLoggedIn: false, token:res.token, udanelogowanie:false}))

        if(this.state.token===undefined) {this.setState({udanelogowanie:false}); alert("Coś poszło nie tak, spróbuj ponownie") ;this.resetForm();  } 
        else{
          this.ID();
          this.setState({isLoggedIn: true, udanelogowanie:true})
        }
        
       
      }
      
      catch(e){
        console.log(e);
        this.resetForm(e);
        alert("Coś poszło nie tak, spróbuj ponownie");
      }
      
      
        

    }

    ID(){
      try{
      fetch('http://localhost:8080/api/users').then(res => res.json())
      .then(json=>this.setState({userIds: json.filter(userId=>userId.userEmail===this.state.emailAddress).map(this.GiveMeThisFuckingId).toString()}))
          
      //this.setState({tempUser:this.state.userIds.map(this.GiveMeThisFuckingId).toString()})

    }
    
    catch(e){
      console.log(e);
      }
    }


    GiveMeThisFuckingId= ulele => {
      return(ulele.userId)
      }

      
    render(){
      console.log(this.state.userIds)
    // var temp = this.state.userIds.map(this.GiveMeThisFuckingId).toString();
    
    if(this.state.isLoggedIn===true){
      if(this.state.userIds!=='')  {
        return(
          <div className="app">
            <div className='container'>
              <div id="logout"><button className="classicButton" onClick={() => this.doLogout()} > 
                Wyloguj
              </button></div>
            <div id={this.state.userIds}><AppLoggedIn userId={this.state.userIds} token={this.state.token}/></div>
            </div>
          </div>

        );
        } 
        else { return("Ładuję")}
      }
  else if(this.state.isLoggedIn===false && this.state.rejsetracja===false){
  return (
    <div className="loginForm">
      <h2>Logowanie</h2>
      <InputFiled
        type='text'
        placeholder='emailAddress'
        value={this.state.emailAddress ? this.state.emailAddress : ''}
        onChange={ (val) => this.srtInputValue('emailAddress', val)}
      />

      <InputFiled
        onChange=''
        type='password'
        placeholder='Password'
        value={this.state.password ? this.state.password : ''}
        onChange={ (val) => this.srtInputValue('password', val)}
      />
      <button className="classicButton" onClick={ () => this.doLogin()} >
              Zaloguj
      </button>
      <button className="classicButton" onClick={ () => this.Registration()} >
              Rejestracja
      </button>     
    </div>
      
  );
  } 
  else if(this.state.rejsetracja===true && this.state.isLoggedIn===false){
    return (
      <div className="loginForm">
        <h2>Logowanie</h2>
        <InputFiled
          type='text'
          placeholder='Email'
          value={this.state.emailAddress ? this.state.emailAddress : ''}
          onChange={ (val) => this.srtInputValue('emailAddress', val)}
        />
  
        <InputFiled
          onChange=''
          type='password'
          placeholder='Hasło'
          value={this.state.password ? this.state.password : ''}
          onChange={ (val) => this.srtInputValue('password', val)}
        />

        <InputFiled
          onChange=''
          type='password'
          placeholder='Powtórz hasło'
          value={this.state.password1 ? this.state.password1 : ''}
          onChange={ (val) => this.srtInputValue('password1', val)}
        />


        <button className="classicButton" onClick={ () => this.doRegistration()}>
                Zarejestruj
        </button>
        <button className="classicButton" onClick={ () => this.doLogout()} >
                Prowrót do strony logowania
        </button>
      
      </div>
        
    );

  }
    
  

}
}

export default LoginForm;
/*
<GoogleLogin
        clientId="711784403029-fsocggbsa1qnpn5412l99v7dvg7t7gdi.apps.googleusercontent.com"
        buttonText="Login"
        onSuccess={this.responseGoogle}
        onFailure={this.responseGoogle}
        cookiePolicy={'single_host_origin'}/>
        */