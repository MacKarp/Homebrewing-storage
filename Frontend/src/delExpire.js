import * as React from 'react';
//import ReactDOM from 'react-dom';

class DelExpire extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        expireId: props.expireId,
        token: props.token,
        postId: null,
        idUser: props.idUser,
      };
    }


   componentDidMount() {
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json-patch+json', 'Authorization' : this.state.token}
        };
        fetch('http://localhost:8080/api/Expire/'+this.state.expireId, requestOptions)

    }




    render(){

        return(
            <span>
                  Usunięto
            </span>
        )
    }
}




export default DelExpire;