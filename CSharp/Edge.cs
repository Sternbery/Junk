using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
namespace polyhedraV3{
	/**
	 *
	 * @author Stefan
	 */
	public class Edge {
		/**
		* Front and back are vertices.
		* Left and right are faces;
		* The following should always be true
		* next.back == this.front 
		* prev.front == this.back
		* next.left == this.left == prev.left
		* this.right == reverse.left;
		*/
		public int f, b, l, r;

		public int F() {
			return f;
		}
		public int B() {
			return b;
		}
		public int L() {
			return l;
		}
		public int R() {
			return r;
		}
		public int N() {
			return n;
		}
		public int P() {
			return p;
		}
		public int Rev() {
			return rev;
		}
		public int Cw() {
			return cw;
		}
		public int Ccw() {
			return ccw;
		}

		//u is about the front
		//d is about the back
		public int n, p, rev, cw, ccw;
		//    Edge next;
		//    Edge prev;
		//    Edge reverse;

		internal int data_val;

		public int data(){
		   return data_val;
		}

		public Edge next<F,E,V>( EdgeHedron<F,E,V> parent ){
			return parent.getEdge(n);
		}
		public Edge prev<F,E,V>( EdgeHedron<F,E,V> parent ){
			return parent.getEdge(p);
		}
		public Edge reverse<F,E,V>( EdgeHedron<F,E,V> parent ){
			return parent.getEdge(rev);
		}
		public Edge clockwise<F,E,V>( EdgeHedron<F,E,V> parent ){
			return parent.getEdge(cw);
		}
		public Edge cClockwise<F,E,V>( EdgeHedron<F,E,V> parent ){
			return parent.getEdge(ccw);
		}
		public E getData <F,E,V>( EdgeHedron<F,E,V> parent ){
			return parent.getE(data_val);
		}
		
		
		public Edge invert<F,E,V>( EdgeHedron<F,E,V> parent ){
			Edge i = new Edge();
			i.l = this.b;
			i.b = this.r;
			i.r= this.f;
			i.f= this.l;
			
			i.n = this.prev(parent).rev;
			i.p = this.reverse(parent).n;
			i.rev = this.rev;
			i.cw = this.p;
			i.ccw = this.n;
			
			return i;
		}
	}
}

