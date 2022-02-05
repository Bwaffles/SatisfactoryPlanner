import React from 'react';
import { Checkbox, Header, Item } from 'semantic-ui-react';
import { OutputList } from './OutputList.jsx';



export class Calculator extends React.Component {
    constructor(props) {
        super(props);

        var outputItem = this.props
            .recipe
            .products
            .find(product => product.id == this.props.item);

        this.state = {
            totalItem: outputItem.itemsPerMinute,
            outputs: this.props.recipe
                .products
                .map(product => {
                    return {
                        id: product.id,
                        name: product.name,
                        originalItemsPerMinute: product.itemsPerMinute,
                        itemsPerMinute: product.itemsPerMinute
                    };
                }),
            inputs: this.props.recipe
                .ingredients
                .map(ingredient => {
                    return {
                        id: ingredient.id,
                        name: ingredient.name,
                        originalItemsPerMinute: ingredient.itemsPerMinute,
                        itemsPerMinute: ingredient.itemsPerMinute,
                        produceOnSite: false
                    };
                })
        };

        this.handleUpdateTotals = this.handleUpdateTotals.bind(this);
        this.handleProduceOnSite = this.handleProduceOnSite.bind(this);
    }

    handleUpdateTotals(event) {
        // TODO seems like if this were a class and internally did this calculation it would be easier
        // need to look into how to create a class and if that works in react states

        var newTotal = event.target.value;

        this.setState(state => {

            var outputItemsPerMinute = state.outputs
                .find(output => output.id == this.props.item)
                .originalItemsPerMinute;

            var productionRatio = newTotal / outputItemsPerMinute;

            return {
                totalItem: newTotal,
                outputs: state.outputs
                    .map(currentOutput => {
                        currentOutput.itemsPerMinute = currentOutput.originalItemsPerMinute * productionRatio;
                        return currentOutput;
                    }),
                inputs: state.inputs
                    .map(currentInput => {
                        currentInput.itemsPerMinute = currentInput.originalItemsPerMinute * productionRatio;
                        return currentInput;
                    })
            }
        });
    }

    handleProduceOnSite(event, input) {
        var produceOnSite = event.target.checked;

        this.setState(state => {
            const inputs = state.inputs.map(currentInput => {
                if (currentInput.id == input.id) {
                    currentInput.produceOnSite = produceOnSite;
                }

                return currentInput;
            });

            return inputs;
        });
    }

    componentDidUpdate(prevProps) {
        if (this.props.recipe === prevProps.recipe) {
            return;
        }

        // Update the recipe that is used
        this.setState({
            totalItem: this.props.recipe
                .products
                .find(product => product.id == this.props.item)
                .itemsPerMinute,
            inputs: this.props.recipe
                .ingredients
                .map(ingredient => {
                    return {
                        id: ingredient.id,
                        name: ingredient.name,
                        itemsPerMinute: ingredient.itemsPerMinute,
                        produceOnSite: false // TODO maybe persist if the same ingredient was present in last recipe
                    };
                })
        });
    }

    render() {

        var recipe = this.props.recipe;
        var outputs = this.state.outputs;
        var inputs = this.state.inputs;

        var outputItem = recipe.products.find(product => product.id == this.props.item);
        var inputsProducedOnSite = inputs.filter(input => input.produceOnSite);

        console.debug("props", this.props);
        console.debug("state", this.state);

        return (

            <div>
                <div className="ui center aligned blue very padded text raised segment">
                    <h2 className="ui horizontal divider header">
                        Calculator
                    </h2>

                    <p><b>Selected recipe:</b> {recipe.name}</p>

                    <div className="ui inline field">
                        <label>Target Output</label>
                        <div className="ui right labeled input">
                            <input
                                type="text"
                                value={this.state.totalItem}
                                onChange={(e) => this.handleUpdateTotals(e)} />
                            <div className="ui label">
                                / min
                            </div>
                        </div>
                    </div>

                    <Header as='h3'>Output</Header>
                    <OutputList outputs={outputs} />

                    <Header as='h3'>Inputs</Header>
                    <Item.Group divided>

                        {inputs.map((input) =>
                            <Item key={input.id}>
                                <Item.Content>
                                    <span className="ui black text" style={{ marginRight: 0.5 + 'rem' }}>{input.name}</span>
                                    <span className="ui grey text">{input.itemsPerMinute}/min</span>
                                </Item.Content>
                                <Item.Extra>
                                    <Checkbox floated="right" toggle
                                        label="Produce on site"
                                        id={input.id + "-produceOnSite"}
                                        checked={input.produceOnSite}
                                        onChange={(e) => this.handleProduceOnSite(e, input)}
                                    />
                                </Item.Extra>
                            </Item>
                        )}

                    </Item.Group>

                    <h3>Machines</h3>
                    {this.state.totalItem / outputItem.itemsPerMinute}
                </div>

                {inputsProducedOnSite.map(input => <div key={input.id} className="ui fluid raised card">
                    <div className="content">
                        <div className="header">
                            {input.name}
                        </div>
                        Something is being produced on site!
                    </div>
                </div>
                )}
            </div>
        );
    }
}
