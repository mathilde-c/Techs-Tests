import React, { Component } from 'react';

class FormLanguageChoice extends Component {
    constructor(props){
        super(props);
        this.state = {
            language: '',
            name: '',
            onSubmit: props.submitHandler,
        }
    }
    
    handleLanguageChange = (event) => {
        this.setState({ language: event.target.value })
    };
    
    handleNameChange = (event) => {
        this.setState({ name: event.target.value })
    }
    
    handleOnSubmit = (event) => {
        event.preventDefault();
        //alert(this.state.name + " you select " + this.state.language + " as your default programming language");
        this.state.onSubmit(this.state.language, this.state.name);
        event.preventDefault();
    }

    render () 
    {
        return (
        <div>
            <form onSubmit={this.handleOnSubmit}>
            <label>Which programming langauge you prefer? </label>
            <select onChange={this.handleLanguageChange}>
                <option value="none" selected disabled hidden> 
                    Select a Language 
                </option> 
                <option value="javascript">JavaScript</option>
                <option value="python">Elm</option>
                <option value="java">Java</option>
                <option value="csharp">C#</option>
                <option value="python">Python</option>
                <option value="swift"> Swift</option>
            </select>
            
            <div style={{ marginTop: '20px' }}>
                <label>Enter your name: </label>
                <input type="text" onChange={this.handleNameChange}/>
            </div>
            
            <input type="submit" value="Submit" />
            </form>
        </div>
        );
    }
}

export default FormLanguageChoice;