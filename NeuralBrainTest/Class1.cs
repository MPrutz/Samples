﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Here's an experiment to see if we can take a Texan's idea, implemented in LUA, and get a practical C# library
//
//REFERENCE
//This started from a youtube demo (Seth BLing) that pastebinned (verb?) script that had ref the concepts in a PDF
//    https://www.youtube.com/watch?v=qv6UVOQ0F44
//    http://pastebin.com/ZZmSNaHX
//    http://nn.cs.utexas.edu/downloads/papers/stanley.ec02.pdf
//
//PDF ABSTRACT: K. O. Stanley and R. (2002) Miikkulainen - Evolving NN’s through Augmenting Topologies 
//      An important question in neuroevolution is how to gain an advantage from evolving
//    neural network topologies along with weights.We present a method, NeuroEvolu-
//    tion of Augmenting Topologies(NEAT), which outperforms the best fixed-topology
//    method on a challenging benchmark reinforcement learning task.We claim that the
//    increased efficiency is due to(1) employing a principled method of crossover of differ -
//    ent topologies, (2) protecting structural innovation using speciation, and(3) incremen-
//    tally growing from minimal structure.We test this claim through a series of ablation
//    studies that demonstrate that each component is necessary to the system as a whole
//    and to each other.  What results is significantly faster learning.NEAT is also an im-
//    portant contribution to GAs because it shows how it is possible for evolution to both
//    optimize and complexify solutions simultaneously, offering the possibility of evolving
//    increasingly complex solutions over generations, and strengthening the analogy with
//    biological evolution.
//
//HISTORY ( oldest last )
// mm/dd/yy author comment
namespace NeuralBrainTest
{
    public class NeuralSystem
    {
        // Species have Genomes have Genes have fitness
        //inputs to the net
        //  define for the system to use
        //  define getting input data
        //pool
        //  new
        //  init
        //  innovation
        //species
        //  speciesIsSameSpecies(genome, genome)
        //  speciesGetFitness(species) ?
        //  speciesAverageFitness(species)
        //  speciesCull(isCanOnlyBeOne) // need to compare to iceFlow to see what they are thinking here
        //  speciesBreedChild(species)
        //  speciesIceflow(Species) // remove things not working - need one joke
        //  speciesWeakRemoval()
        //  speciesAddTo(child)
        // getAverageFitnessTotal() ??nesc??
        //genome
        //  new
        //  copy
        //  fitness
        //Gene
        //  NEW
        //  copy(gene)
        //  disjoint(gene, gene)
        //  weights(gene, gene)
        //  isGeneLinked(genes, gene)
        //neuron //inputs
        //  new
        //  RandomNeuron(genes, input)
        //Network has neurons of inputs and outputs
        //  network(genome)
        //  evaluateNetwork(network, inputs)
        //Crossover(genome, genome)
        //mutatePoint(genome)
        //mutateLink(genome, isBiased)
        //mutateNode(genome)
        //mutateEnableDisable(genome, isEnabled)
        //mutate(genome)
        //rank
        //generation()
        //  new
        //Worker Utilities
        //  clear input
        //  initialize Run
        //  evaluate current
        //  display genome
        //  write file
        //  load file
        //  save pool
        //  load pool
        //  sigmoid(#)
        
    }
}
