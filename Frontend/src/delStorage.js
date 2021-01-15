import * as React from 'react';
//import ReactDOM from 'react-dom';

class DelStorage extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        storageId: props.storageId,
        token: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibWFpbEBvMi5wbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im1haWxAbzIucGwiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTYxMTA4MDk1OX0.GElal5oY0zodHyRh3_-jDkjyiPVAQkPYBw1NScwFlkY',
        postId: null,
        idUser: props.idUser,
      };
    }


   componentDidMount() {
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json-patch+json', 'KEY': 'Authorization', 'VALUE': this.state.token}
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