import React, { Component } from 'react';
import FormLanguageChoice from './FormLanguageChoice';

class Choices extends Component {
    state = {
        previousChoice: [],
    }

    submitHandler(language, name){
        this.setState({
            previousChoice: this.state.previousChoice.concat([{
                language: language,
                name: name,
            }]),
        });
    }

    render () {
        return (
            <React.Fragment>
                <FormLanguageChoice submitHandler={(language, name) => this.submitHandler(language, name)}/>
                <ul>
                    {this.state.previousChoice.map( (element,index) =>
                        <li key={index}>{element.name} selected {element.language} as his default language.</li>
                    )}
                </ul>
            </React.Fragment>
        );
    }
};

export default Choices;