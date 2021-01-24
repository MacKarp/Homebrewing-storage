import * as React from 'react';
//import ReactDOM from 'react-dom';

class DelStorage extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        storageId: props.storageId,
        token: props.token,
        postId: null,
        idUser: props.idUser,
      };
    }


   componentDidMount() {
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json-patch+json', 'Authorization': this.state.token}
        };
        fetch('http://localhost:8080/api/Storage/'+this.state.storageId, requestOptions)

    }




    render(){

        return(
            <div id="addStorage">
                  Usunięto
            </div>
        )
    }
}




export default DelStorage;