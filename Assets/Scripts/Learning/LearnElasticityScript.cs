using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnElasticityScript : MonoBehaviour
{
    public Graph graph;
    public Text elasticityTitle;
    public Text elastricityDescription;


    // Update is called once per frame

    public void ClickedElasticDemand()
    {
        graph.LearnDemandElasticity();
        elasticityTitle.text = "Elasticity of demand";
        elastricityDescription.text = @"
When the price of a good goes up, the demand for that good goes down by a relatively large amount. 
When the price of a good goes down, the demand for that product goes up by a relatively large amount
            
Goods with inelastic demand include luxury goods, non-essential services and any good with many subtitutes.
";
    }

    public void ClickedInelasticDemand()
    {
        graph.LearnDemandInelasticity();
        elasticityTitle.text = "Inelasticity of demand";
        elastricityDescription.text = @"
When the price of a good goes up, the supply for that good goes down by a relatively small amount. 
When the price of a good goes down, the supply for that good goes up by a relatively small amount

Goods with elastic demand include necessities such as food and water, medication, and utilities like electricity.
";
    }

    public void ClickedElasticSupply()
    {
        graph.LearnSupplyElasticity();
        elasticityTitle.text = "Elasticity of supply";
        elastricityDescription.text = @"
When the price of a good goes up, the supply of that good goes down by a relatively large amount. 
When the price of a good goes down, the supply of that good goes up by a relatively large amount

Goods with elastic supply include electronics, fast-growing crops, unskilled labor.
";
    }

    public void ClickedInelasticSupply()
    {
        graph.LearnSupplyInelasticity();
        elasticityTitle.text = "Inelasticity of supply";
        elastricityDescription.text = @"
When the price of a good goes up, the supply of that good goes down by a relatively small amount. 
When the price of a good goes down, the supply of that good goes up by a relatively small amount

Goods with inelastic supply includes natural resources such as oil, specialised equipment, skilled labour.";
    }
}
