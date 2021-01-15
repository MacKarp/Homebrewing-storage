import * as React from 'react';
//import ReactDOM from 'react-dom';

class AddStorage extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        token: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibWFpbEBvMi5wbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im1haWxAbzIucGwiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTYxMTA4MDk1OX0.GElal5oY0zodHyRh3_-jDkjyiPVAQkPYBw1NScwFlkY',
        postId: null,
        idUser: props.idUser,
        nameStorage: props.nameStorage
      };
    }

   componentDidMount() {
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json-patch+json', 'KEY': 'Authorization', 'VALUE': this.state.token},
            body: JSON.stringify({ userId: this.state.idUser, storageName: this.state.nameStorage })
        };
        fetch('http://localhost:8080/api/Storage', requestOptions)
            .then(res => res.json())
            .then(data => this.setState({ postId: data.id }))
    }




    render(){

        return(
            <div id="addStorage">
                    Dodano!
            </div>
        )
    }
}




export default AddStorage;